using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using GGuerra.Cardamatic.CardReader.Common.Cards.Base;
using GGuerra.Cardamatic.CardReader.Common.Enum;
using GGuerra.Cardamatic.CardReader.Interface.Facade;
using GGuerra.Cardamatic.Encoding.Interface.Facade;
using GGuerra.Cardamatic.Extensions;
using GGuerra.Cardamatic.WinForm.Constants;
using GGuerra.Cardamatic.WinForm.KeySets;
using GGuerra.Cardamatic.WinForm.KeySets.Dto;
using GGuerra.Cardamatic.WinForm.Schemas;
using GGuerra.Cardamatic.WinForm.Schemas.Dto;


namespace GGuerra.Cardamatic.WinForm.Application
{
    public partial class FormMain : Form
    {
        private readonly ILogger<FormMain> _logger;

        private readonly ISchemaFactory _schemaFactory;
        private readonly IKeySetFactory _keySetFactory;

        private readonly IDecoderManager _decoderManager;
        private readonly ICardReaderManager _cardReaderManager;
        private readonly IMifareClassicService _mifareClassicService;
        private readonly IDesfireService _desfireService;
        private readonly IEnumerable<ISamService> _samServices;
        private ContactlessRaw _card;

        private string _cardPath;
        private ICardReaderDevice _cardReader;
        private Schema _selectedSchema => ((Schema)schemaComboBox.SelectedItem);
        private KeySet _selectedKeySet => ((KeySet)keysetComboBox.SelectedItem);

        public FormMain(
            ILogger<FormMain> logger,
            ISchemaFactory schemaFactory,
            IKeySetFactory keySetFactory,
            IDecoderManager decoderManager,
            ICardReaderManager cardReaderManager,
            IMifareClassicService mifareClassicService,
            IDesfireService desfireService,
            IEnumerable<ISamService> samServices
            )
        {
            _logger = logger;
            _schemaFactory = schemaFactory;
            _keySetFactory = keySetFactory;
            _decoderManager = decoderManager;
            _cardReaderManager = cardReaderManager;
            _mifareClassicService = mifareClassicService;
            _desfireService = desfireService;
            _samServices = samServices;

            InitializeComponent();

            dataTabControl.TabPages.Clear();
        }

        #region Menu loading

        private void FormMain_Load(object sender, EventArgs e)
        {
            // Load schemas
            LoadSchemas();

            // Load key sets
            LoadKeySets();

            // Load card readers
            LoadCardReaders();

            readToolStripMenuItem.Enabled = false;
            writeToolStripMenuItem.Enabled = false;
        }

        private void LoadSchemas()
        {
            foreach (var filePath in Directory.EnumerateFiles(ApplicationPaths.SchemasPath, ApplicationFilters.JsonFilter))
            {
                try
                {
                    var schema = _schemaFactory.GetSchema(filePath);
                    schemaComboBox.Items.Add(schema);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    MessageBox.Show(ex.Message, "ERROR");
                }
            }
        }

        private void LoadKeySets()
        {
            try
            {
                _keySetFactory.Initialize(ApplicationPaths.KeysetsPath, ApplicationFilters.KeysFilter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                MessageBox.Show(ex.Message, "ERROR");
            }
        }

        private void LoadCardReaders()
        {
            foreach (var cardReader in _cardReaderManager.CardReaders)
            {
                var cardReaderStripMenuItem = new ToolStripMenuItem()
                {
                    Name = cardReader.DeviceName,
                    Text = cardReader.DeviceName,
                    Tag = cardReader,
                };
                cardReaderStripMenuItem.Click += CardReaderStripMenuItem_Click;
                if (_cardReader == null)
                {
                    _cardReader = cardReader;
                    _cardReader.CardDetected += CardReader_CardDetected;
                    _cardReader.CardRemoved += CardReader_CardRemoved;
                    _cardReader.StartDetection();
                    cardReaderStripMenuItem.Checked = true;
                }
                pickToolStripMenuItem.DropDownItems.Add(cardReaderStripMenuItem);
            }
        }


        #endregion

        #region Menu events
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var t = new Thread((ThreadStart)(() =>
                {
                    var openFileDialog = new OpenFileDialog
                    {
                        InitialDirectory = ApplicationPaths.CardsPath,
                        Filter = ApplicationFilters.CardImagesDialogFilter
                    };
                    var result = openFileDialog.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        _cardPath = openFileDialog.FileName;

                        var fileStream = new FileStream(_cardPath, FileMode.Open, FileAccess.Read);
                        var streamReader = new StreamReader(fileStream);
                        var cardContent = streamReader.ReadToEnd();
                        streamReader.Close();
                        fileStream.Close();

                        _card = JsonConvert.DeserializeObject<ContactlessRaw>(cardContent);
                    }
                }));
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
                t.Join();

                FillSchema();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var t = new Thread((ThreadStart)(() =>
            {
                var saveFileDialog = new SaveFileDialog
                {
                    InitialDirectory = ApplicationPaths.CardsPath,
                    Filter = ApplicationFilters.CardImagesDialogFilter
                };
                var result = saveFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    SaveCard(saveFileDialog.FileName);
                }
            }));
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveCard(_cardPath);
        }

        private void SaveCard(string fileName)
        {
            try
            {
                UpdateCard();

                var cardContent = JsonConvert.SerializeObject(_card, Formatting.Indented);

                var fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                var streamWriter = new StreamWriter(fileStream);
                streamWriter.Write(cardContent);
                streamWriter.Close();
                fileStream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ReadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _card = new ContactlessRaw();
            var cardUid = _cardReader.GetCardUid();
            _card.Addresses.Clear();
            _card.Uid = cardUid;

            try
            {
                if (string.IsNullOrEmpty(cardUid))
                {
                    throw new Exception("No card found on the reader.");
                }
                switch (_selectedSchema.ContactlessTechnology)
                {
                    case ContactlessTechnology.MifareClassic:
                        ReadMifareClassicCard();
                        break;
                    case ContactlessTechnology.Desfire:
                        ReadDesfireCard();
                        break;
                    case ContactlessTechnology.Ultralight:
                        ReadUltralightCard();
                        break;
                    default:
                        break;
                }

                FillSchema();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
            }
        }

        private void CardReaderStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in pickToolStripMenuItem.DropDownItems)
            {
                if (item is ToolStripMenuItem menuItem)
                {
                    if (menuItem.Checked)
                    {
                        menuItem.Checked = false;
                        var cardReader = (ICardReaderDevice)menuItem.Tag;
                        cardReader.CardDetected -= CardReader_CardDetected;
                        cardReader.CardRemoved -= CardReader_CardRemoved;
                        cardReader.StopDetection();
                    }
                }
            }
            if (sender is ToolStripMenuItem menuItemSender)
            {
                menuItemSender.Checked = true;
                _cardReader = (ICardReaderDevice)menuItemSender.Tag;
                _cardReader.CardDetected += CardReader_CardDetected;
                _cardReader.CardRemoved += CardReader_CardRemoved;
                _cardReader.StartDetection();
            }
        }

        private void SchemaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                UpdateKeySets();

                FillSchema();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
            }
        }

        private void UpdateKeySets()
        {
            var selectedKeySet = _selectedKeySet;
            keysetComboBox.Items.Clear();
            if (_selectedSchema.ContactlessTechnology == ContactlessTechnology.MifareClassic)
            {
                foreach (var keySet in _keySetFactory.KeySets.Where(k => k.MifareClassicKeys != null))
                {
                    keysetComboBox.Items.Add(keySet);
                }
            }
            if (_selectedSchema.ContactlessTechnology == ContactlessTechnology.Desfire)
            {
                foreach (var keySet in _keySetFactory.KeySets.Where(k => k.DesfireKeys != null))
                {
                    keysetComboBox.Items.Add(keySet);
                }
            }
            if(_selectedSchema.ContactlessTechnology == ContactlessTechnology.Ultralight)
            {
                // No keys needed
                readToolStripMenuItem.Enabled = true;
                writeToolStripMenuItem.Enabled = true;
            }
            keysetComboBox.SelectedItem = selectedKeySet;
        }


        private void KeysetCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cardReader != null)
            {
                if (_selectedSchema.ContactlessTechnology == ContactlessTechnology.MifareClassic)
                {
                    _mifareClassicService.SetCardReader(_cardReader.GetContactlessCardReader());
                    foreach (var mifareClassicKey in _selectedKeySet.MifareClassicKeys)
                    {
                        uint.TryParse(mifareClassicKey.Address, out uint sector);
                        _mifareClassicService.LoadKey(mifareClassicKey.KeyNo, sector, mifareClassicKey.KeyType, mifareClassicKey.Key);
                    }
                }
                if (_selectedSchema.ContactlessTechnology == ContactlessTechnology.Desfire)
                {
                    _desfireService.SetCardReader(_cardReader.GetContactlessCardReader());
                }
            }
            EnableReadAndWrite();
        }

        private void EnableReadAndWrite()
        {
            if (_selectedSchema != null && keysetComboBox.SelectedItem != null && _cardReader != null)
            {
                readToolStripMenuItem.Enabled = true;
                writeToolStripMenuItem.Enabled = true;
            }
        }

        #endregion

        #region Schema events
        private void DataGroupBox_SizeChanged(object sender, EventArgs e)
        {
            FillSchema();
        }

        private void PropertyValue_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox txtBoxSender)
            {
                var parser = _decoderManager.GetParseable(((SchemaProperty)txtBoxSender.Tag).TypeName).GetParser();
                ((SchemaProperty)txtBoxSender.Tag).Value = parser(txtBoxSender.Text);
            }
        }

        private void WriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateCard();
                var cardUid = _cardReader.GetCardUid();
                if (string.IsNullOrEmpty(cardUid))
                {
                    throw new Exception("No card found on the reader.");
                }
                switch (_selectedSchema.ContactlessTechnology)
                {
                    case ContactlessTechnology.MifareClassic:
                        WriteMifareClassicCard();
                        break;
                    case ContactlessTechnology.Desfire:
                        WriteDesfireCard();
                        break;
                    case ContactlessTechnology.Ultralight:
                        WriteUltralightCard();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
            }
        }


        #endregion

        #region Schema methods
        private void FillSchema()
        {
            dataTabControl.TabPages.Clear();
            if (_selectedSchema != null)
            {
                foreach (var schemaTab in _selectedSchema.Tabs)
                {
                    dataTabControl.TabPages.Add(schemaTab.Description);
                    var currentTab = dataTabControl.TabPages[dataTabControl.TabPages.Count - 1];

                    int columnOrder = 0;
                    foreach (var schemaColumn in schemaTab.Columns)
                    {
                        var columnWidht = dataTabControl.Width / schemaTab.Columns.Count;
                        var groupBox = new GroupBox()
                        {
                            Text = schemaColumn.Description,
                            Tag = schemaColumn,
                            Size = new Size(columnWidht, dataTabControl.Height),
                            Location = new Point(columnOrder * (columnWidht), 0)
                        };
                        currentTab.Controls.Add(groupBox);
                        var currentColumn = currentTab.Controls[columnOrder];

                        int propertyOrder = 0;
                        const int propertyRightMargin = 12;
                        const int propertyHeight = 30;
                        foreach (var schemaProperty in schemaColumn.Properties)
                        {
                            var propertyLabel = new Label()
                            {
                                Text = schemaProperty.Name,
                                Location = new Point(propertyRightMargin, (propertyOrder + 1) * propertyHeight)
                            };
                            var propertyTxtBox = new TextBox()
                            {
                                Location = new Point(propertyLabel.Location.X + propertyLabel.Width, propertyLabel.Location.Y)
                            };
                            propertyTxtBox.Enabled = schemaProperty.Algorithm == null;

                            if (_card != null)
                            {
                                SetTextBoxValue(propertyTxtBox, schemaProperty);
                            }

                            currentColumn.Controls.Add(propertyLabel);
                            currentColumn.Controls.Add(propertyTxtBox);
                            propertyOrder++;
                        }
                        columnOrder++;
                    }
                }
            }
        }

        
        private void SetTextBoxValue(TextBox propertyValue, SchemaProperty schemaProperty)
        {
            try
            {
                var decodable = _decoderManager.GetDecodable(schemaProperty.TypeName);
                var decoder = decodable.GetDecoder();
                var cardProperty = decoder(_card.Addresses[schemaProperty.Address.ToString()].ToByteArray(), schemaProperty.Offset, schemaProperty.OffSetBits, schemaProperty.Length, schemaProperty.LengthBits);
                schemaProperty.Value = cardProperty;
                propertyValue.Tag = schemaProperty;
                propertyValue.Text = cardProperty.ToString();
                if (decodable.TextLengthFixed)
                {
                    propertyValue.MaxLength = propertyValue.Text.Length;
                }
                propertyValue.TextChanged += PropertyValue_TextChanged;
            }
            catch(Exception ex)
            {
                throw new Exception($"Cannot decode {schemaProperty.Name} of type {schemaProperty.TypeName}.{Environment.NewLine}{ex.Message}");
            }
        }

        private void UpdateCard()
        {
            foreach (Control tabPage in dataTabControl.Controls)
            {
                foreach (Control column in tabPage.Controls)
                {
                    foreach (Control txtBox in column.Controls)
                    {
                        if (txtBox as TextBox != null)
                        {
                            UpdateCardField(txtBox);
                        }
                    }
                }
            }
        }


        private void UpdateCardField(Control txtBox)
        {
            var schemaProperty = (SchemaProperty)txtBox.Tag;
            try
            {
                var cardProperty = schemaProperty.Value;
                var encoder = _decoderManager.GetEncodable(schemaProperty.TypeName).GetEncoder();

                var addressBytes = _card.Addresses[schemaProperty.Address].ToByteArray();
                if (schemaProperty.Algorithm == null)
                {
                    if (schemaProperty.LengthBits == 0)
                    {
                        var cardPropertyBytes = encoder(cardProperty, (int)schemaProperty.Length, schemaProperty.LengthBits);
                        Array.Copy(cardPropertyBytes, 0, addressBytes, schemaProperty.Offset, schemaProperty.Length);
                    }
                    else
                    {
                        var cardPropertyBytes = encoder(cardProperty, (int)schemaProperty.LengthBits / 8 + 1, schemaProperty.LengthBits);
                        var cardPropertyBits = cardPropertyBytes.GetBits(schemaProperty.LengthBits);
                        var contentBits = addressBytes.GetBits();

                        for (int i = 0; i < schemaProperty.LengthBits; i++)
                        {
                            contentBits[schemaProperty.Offset * 8 + schemaProperty.OffSetBits + i] = cardPropertyBits[i];
                        }
                        addressBytes = contentBits.FromBits();
                    }
                }
                else
                {
                    if (schemaProperty.Algorithm.LenghtBits == 0)
                    {
                        if (schemaProperty.Algorithm != null)
                        {
                            var algorithmBuffer = new byte[schemaProperty.Algorithm.Length];
                            Array.Copy(addressBytes, schemaProperty.Algorithm.Offset, algorithmBuffer, 0, schemaProperty.Algorithm.Length);
                            var cardPropertyBytes = encoder(algorithmBuffer, (int)schemaProperty.Algorithm.Length, schemaProperty.LengthBits);
                            Array.Copy(cardPropertyBytes, 0, addressBytes, schemaProperty.Offset, schemaProperty.Length);
                        }
                    }
                }
                _card.Addresses[schemaProperty.Address] = addressBytes.ToHexString();
            }
            catch (Exception ex)
            {
                throw new Exception($"Cannot encode {schemaProperty.Name} of type {schemaProperty.TypeName} value {schemaProperty.Value}.{Environment.NewLine}{ex.Message}");
            }
        }

        #endregion

        #region Card events
        private void CardReader_CardRemoved(object sender)
        {
            try
            {
                Invoke(new Action(() => uidLabel.Text = "-"));
            }
            catch (Exception) { }
        }

        private void CardReader_CardDetected(object sender, string uid)
        {
            try
            {
                Invoke(new Action(() => uidLabel.Text = uid));
            }
            catch (Exception) { }
        }
        #endregion

        #region Card methods
        private void ReadMifareClassicCard()
        {
            // Set reader.
            _mifareClassicService.SetCardReader(_cardReader.GetContactlessCardReader());

            foreach (var mifareClassicKey in _selectedKeySet.MifareClassicKeys)
            {
                uint.TryParse(mifareClassicKey.Address, out uint sector);
                _mifareClassicService.LoadKey(mifareClassicKey.KeyNo, sector, mifareClassicKey.KeyType, mifareClassicKey.Key);
            }

            for (uint sector = 0; sector < 16; sector++)
            {
                var key = _selectedKeySet.MifareClassicKeys.FirstOrDefault(k => k.Address == sector.ToString() && k.ReadKey);
                if (key != null)
                {
                    var auth = _mifareClassicService.Authenticate(key.KeyNo, sector, key.KeyType);
                    if (auth)
                    {
                        var blocks = new List<uint>();
                        for (uint order = 0; order < 4; order++)
                        {
                            var block = sector * 4 + order;
                            blocks.Add(block);
                        }
                        var readedAddresses = _mifareClassicService.ReadBlocks(blocks);
                        if (readedAddresses.Any())
                        {
                            foreach (var dataReaded in readedAddresses)
                            {
                                _card.Addresses.Add(dataReaded.Key.ToString(), dataReaded.Value);
                            }
                        }
                        else
                        {
                            throw new Exception($"Cannot read blocks: {blocks}.");
                        }
                    }
                    else
                    {
                        throw new Exception($"Invalid key. Cannot authenticate sector {sector}.");
                    }
                }
            }
        }

        private void ReadDesfireCard()
        {
            // Set reader.
            _desfireService.SetCardReader(_cardReader.GetContactlessCardReader());
            // Set samService.
            _desfireService.SetSamService(_samServices.FirstOrDefault( s=> s.Name == _selectedKeySet.SamService));
            // Reset card authentication
            _desfireService.ResetAuthentication();

            var cardUid = _cardReader.GetCardUid();
            var cardVersion = _desfireService.GetCardVersion();
            var applications = _desfireService.GetApplications();

            if (!applications.Any())
            {
                throw new Exception($"No applications found in card.");
            }

            foreach (var application in applications)
            {
                var appSelected = _desfireService.SelectApplication(application.ApplicationId);
                if (appSelected)
                {
                    var keyVersions = _desfireService.GetKeyVersions();

                    var files = _desfireService.GetFiles();
                    var fileIds = _desfireService.GetFileIds();
                    foreach (var fileId in fileIds)
                    {
                        var fileSettings = _desfireService.GetFileSettings(fileId);
                    }
                    var key = _selectedKeySet.DesfireKeys.FirstOrDefault(k => k.Address == application.ApplicationId && k.ReadKey);
                    if (key != null)
                    {
                        var auth = _desfireService.Authenticate(key.KeyNo, key.Key, GetDesfireAuthenticationType(key.ApplicationCrypto), cardUid);
                        if (auth)
                        {
                            foreach (var fileId in fileIds)
                            {
                                var fileData = _desfireService.ReadFile(fileId, DesfireFileType.Data, 0, 0 /*fileSettings.FileLength*/, DesfireCommunicationMode.Enciphered);
                                if (!string.IsNullOrEmpty(fileData))
                                {
                                    _card.Addresses.Add($"{application.ApplicationId}{fileId:X2}", fileData);
                                }
                                else
                                {
                                    //throw new Exception($"Cannot read file {fileId} in application: {application.ApplicationId}.");
                                }
                            }
                        }
                        else
                        {
                            throw new Exception($"Invalid key. Cannot authenticate application: {application.ApplicationId}.");
                        }
                    }
                    else
                    {
                        throw new Exception($"Key not found for application: {application.ApplicationId}.");
                    }
                }
            }
        }

        private void ReadUltralightCard()
        {
            // Set reader.
            _mifareClassicService.SetCardReader(_cardReader.GetContactlessCardReader());

            for (uint sector = 0; sector < 4; sector++)
            {
                var blocks = new List<uint>();
                for (uint order = 0; order < 4; order++)
                {
                    var block = sector * 4 + order;
                    blocks.Add(block);
                }
                var readedAddresses = _mifareClassicService.ReadBlocks(blocks);
                if (readedAddresses.Any())
                {
                    foreach (var dataReaded in readedAddresses)
                    {
                        _card.Addresses.Add(dataReaded.Key.ToString(), dataReaded.Value);
                    }
                }
                else
                {
                    throw new Exception($"Cannot read blocks: {blocks}.");
                }
            }
        }

        private void WriteMifareClassicCard()
        {
            // Set reader.
            _mifareClassicService.SetCardReader(_cardReader.GetContactlessCardReader());

            foreach (var mifareClassicKey in _selectedKeySet.MifareClassicKeys)
            {
                uint.TryParse(mifareClassicKey.Address, out uint sector);
                _mifareClassicService.LoadKey(mifareClassicKey.KeyNo, sector, mifareClassicKey.KeyType, mifareClassicKey.Key);
            }

            for (uint sector = 0; sector < 16; sector++)
            {
                var key = _selectedKeySet.MifareClassicKeys.FirstOrDefault(k => k.Address == sector.ToString() && k.WriteKey);
                if (key != null)
                {
                    var auth = _mifareClassicService.Authenticate(key.KeyNo, sector, key.KeyType);
                    if (auth)
                    {
                        uint minBlock = 0;
                        if (sector == 0)
                        {
                            minBlock = 1;
                        }
                        var dataBlocks = new Dictionary<uint, string>();
                        for (uint order = minBlock; order < 3; order++)
                        {
                            var block = sector * 4 + order;
                            dataBlocks.Add(block, _card.Addresses[block.ToString()]);
                        }
                        var updatedBlocks = _mifareClassicService.WriteBlocks(dataBlocks);
                    }
                    else
                    {
                        throw new Exception($"Invalid key. Cannot authenticate sector {sector}.");
                    }
                }
            }
        }

        private void WriteDesfireCard()
        {
            // Set reader.
            _desfireService.SetCardReader(_cardReader.GetContactlessCardReader());
            // Set samService.
            _desfireService.SetSamService(_samServices.FirstOrDefault(s => s.Name == _selectedKeySet.SamService));
            // Reset card authentication
            _desfireService.ResetAuthentication();

            var cardUid = _cardReader.GetCardUid();
            var cardVersion = _desfireService.GetCardVersion();
            var applications = _desfireService.GetApplications();


            if (!applications.Any())
            {
                throw new Exception($"No applications found in card.");
            }

            foreach (var application in applications)
            {
                var appSelected = _desfireService.SelectApplication(application.ApplicationId);
                if (appSelected)
                {
                    var keyVersions = _desfireService.GetKeyVersions();

                    var files = _desfireService.GetFiles();
                    var fileIds = _desfireService.GetFileIds();
                    foreach (var fileId in fileIds)
                    {
                        var fileSettings = _desfireService.GetFileSettings(fileId);
                    }
                    var key = _selectedKeySet.DesfireKeys.FirstOrDefault(k => k.Address == application.ApplicationId && k.WriteKey);
                    if (key != null)
                    {
                        var auth = _desfireService.Authenticate(key.KeyNo, key.Key, GetDesfireAuthenticationType(key.ApplicationCrypto), cardUid);
                        if (auth)
                        {
                            foreach (var fileId in fileIds)
                            {
                                _card.Addresses.TryGetValue($"{application.ApplicationId}{fileId:X2}", out string fileData);
                                if (!string.IsNullOrEmpty(fileData))
                                {
                                    var result = _desfireService.WriteFile(fileData, fileId, DesfireFileType.Data, 0, fileData.Length / 2, DesfireCommunicationMode.Enciphered);
                                    if (!result)
                                    {
                                        throw new Exception($"Cannot write file {fileId} in application: {application.ApplicationId}.");
                                    }
                                }
                            }
                            var commited = _desfireService.CommitTransaction();
                            if(!commited)
                            {
                                //throw new Exception($"Cannot commit transaction.");
                            }
                        }
                        else
                        {
                            throw new Exception($"Invalid key. Cannot authenticate application: {application.ApplicationId}.");
                        }
                    }
                    else
                    {
                        throw new Exception($"Key not found for application: {application.ApplicationId}.");
                    }
                }
            }
        }

        private void WriteUltralightCard()
        {
            // Set reader.
            _mifareClassicService.SetCardReader(_cardReader.GetContactlessCardReader());

            for (uint sector = 0; sector < 4; sector++)
            {
                if (sector != 0)
                {
                    var dataBlocks = new Dictionary<uint, string>();
                    for (uint order = 0; order < 3; order++)
                    {
                        var block = sector * 4 + order;
                        dataBlocks.Add(block, _card.Addresses[block.ToString()]);
                    }
                    var updatedBlocks = _mifareClassicService.WriteBlocks(dataBlocks);
                }
            }
        }

        private DesfireAuthenticationType GetDesfireAuthenticationType(DesfireApplicationCrypto desfireApplicationCrypto)
        {
            DesfireAuthenticationType result;
            switch (desfireApplicationCrypto)
            {
                case DesfireApplicationCrypto.CryptoNative:
                    result = DesfireAuthenticationType.IsoAuthentication;
                    break;
                case DesfireApplicationCrypto.Crypto3K3Des:
                    result = DesfireAuthenticationType.IsoAuthentication;
                    break;
                case DesfireApplicationCrypto.CryptoAes:
                    result = DesfireAuthenticationType.AesAuthentication;
                    break;
                default:
                    result = DesfireAuthenticationType.IsoAuthentication;
                    break;
            }
            return result;
        }

        #endregion
    }
}
