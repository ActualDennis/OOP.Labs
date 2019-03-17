using JewelryOop;
using JewelryStore.main;
using JewelryStore.main.Factories;
using JewelryStore.main.Plugins;
using JewelryStore.main.Serialization;
using JewelryStore.UI.Data;
using JewelryStore.UI.Helpers;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using System.Xml.Serialization;

namespace JewelryStore.UI.ViewModels {

    public class ApplicationViewModel : BaseViewModel {

        public ApplicationViewModel()
        {
            addMaterialsView = new AddMaterialsViewModel();
            editJewelryView = new EditJewelryViewModel(addMaterialsView, () => EditJewelry_MaterialEdited(), () => EditJewelry_EditMaterial());
            CurrentJewelryMaterials = new List<Material>();
            JewelryList = new List<Jewelry>();
            JewelryListUI = new ObservableCollection<Jewelry>();
            jewelryFactory = new JewelryFactory();
            materialsFactory = new MaterialsFactory();
            Plugins = PluginsLoader<IJewelryEncodingPlugin>.Load();
            serializersFactory = new JewelrySerializersFactory();
        }

        public int CurrentAppScreen { get; set; }

        public string SelectedJewelryType { get; set; }

        public string JewelryName { get; set; }

        public string FoolRatioPercents { get; set; }

        public string[] Materials { get; set; } = new string[] { "Gemstone", "Premium Gemstone", "Premium Material" };

        public string ChosenMaterial { get; set; }

        public int SelectedPluginIndex { get; set; }

        public string[] JewelryTypes { get; set; } = new string[] { "Jewelry", "Bijouterie" };

        public ICommand AddJewelryCommand => new RelayCommand(() => AddJewelry(null), null);

        public ICommand AddMaterialCommand => new RelayCommand(() => AddMaterial(null), null);

        public ICommand EditJewelryCommand => new RelayCommand(() => EditJewelry(null), null);

        public ICommand GotoInitialPageCommand => new RelayCommand(() => GotoInitialPage(null), null);

        public ICommand DeleteJewelryCommand => new RelayCommand(() => DeleteJewelry(null), null);

        public ICommand SerializeCommand => new RelayCommand(() => Serialize(null), null);

        public ICommand DeserializeCommand => new RelayCommand(() => Deserialize(null), null);

        private JewelryFactory jewelryFactory { get; set; }

        private MaterialsFactory materialsFactory { get; set; }

        private JewelrySerializersFactory serializersFactory { get; set; }

        public AddMaterialsViewModel addMaterialsView { get; set; }

        public EditJewelryViewModel editJewelryView { get; set; }

        private List<Material> CurrentJewelryMaterials { get; set; }

        public List<Jewelry> JewelryList { get; set; }

        public ObservableCollection<Jewelry> JewelryListUI { get; set; }


        public List<IJewelryEncodingPlugin> Plugins { get; set; }

        public string SelectedJewelry { get; set; }


        private void AddMaterial(Object parameter)
        {
            try
            {
                var materialToAdd = materialsFactory.NewMaterial(ChosenMaterial);
                CurrentJewelryMaterials.Add(materialToAdd);
                addMaterialsView.AlterMaterial(ref materialToAdd,() => MaterialAlteredCallback());
                CurrentAppScreen = (int)ApplicationScreen.CreateMaterial;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Could not add material. Reason: {ex.Message}");
            }
        }

        private void EditJewelry(Object parameter)
        {
            
            try
            {
                if (SelectedJewelry == string.Empty)
                {
                    MessageBox.Show("Please choose jewelry.");
                    return;
                }

                var editedIndex = JewelryList.FindIndex(x => x.ToString() == SelectedJewelry);
                Jewelry editedJewelry = JewelryList.Find(x => x.ToString() == SelectedJewelry);

                if(editedJewelry == null)
                {
                    MessageBox.Show($"Choose one jewerly from dropdown menu.");
                    return;
                }

                editJewelryView.InitJewelryFields(ref editedJewelry);

                JewelryList.RemoveAt(editedIndex);
                JewelryListUI.RemoveAt(editedIndex);
                JewelryList.Add(editedJewelry);
                JewelryListUI.Add(editedJewelry);

                CurrentAppScreen = (int)ApplicationScreen.EditJewelry;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not edit jewerly. Reason: {ex.Message}");
            }
        }

        private void AddJewelry(Object parameter)
        {
            try
            {
                CurrentJewelryMaterials.RemoveAll(x => !IsMaterialAlterred(x));

                if (CurrentJewelryMaterials.Count == 0)
                {
                    MessageBox.Show("No materials were choosen for this jewelry. Please add some.");
                    return;
                }

                if (string.IsNullOrEmpty(JewelryName))
                {
                    MessageBox.Show("Please choose some name for your jewelry.");
                    return;
                }

                var jewelry = jewelryFactory.NewJewelry(SelectedJewelryType);

                jewelry.Name = JewelryName;
                jewelry.Materials = new List<Material>(CurrentJewelryMaterials);

                if (jewelry is Bijouterie)
                {
                    ((Bijouterie)jewelry).FoolRatio = double.Parse(FoolRatioPercents) / 100;
                }

                MessageBox.Show($"Successfully added {jewelry.Name}.");

                JewelryList.Add(jewelry);

                JewelryListUI.Add(jewelry);

                ClearFields();

                CurrentJewelryMaterials = null;
                CurrentJewelryMaterials = new List<Material>();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Can't add jewelry. Reason: {ex.Message}.");
            }
        }

        private void DeleteJewelry(object p)
        {
            if(string.IsNullOrEmpty(SelectedJewelry))
            {
                MessageBox.Show("Please choose a jewelry from the list.");
                return;
            }

            try
            {
                var removalIndex = JewelryList.FindIndex(x => x.ToString() == SelectedJewelry);
                JewelryList.RemoveAt(removalIndex);
                JewelryListUI.RemoveAt(removalIndex);
                MessageBox.Show($"Successfully removed.");
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Could not remove jewelry. Reason: {ex.Message}");
            }
        }

        private void GotoInitialPage(object p)
        {
            CurrentAppScreen = (int)ApplicationScreen.Initial;
        }

        private void ClearFields()
        {
            ChosenMaterial = string.Empty;
            SelectedJewelryType = string.Empty;
            JewelryName = string.Empty;
            FoolRatioPercents = string.Empty;
        }

        private void MaterialAlteredCallback()
        {
            CurrentAppScreen = (int)ApplicationScreen.Initial;
        }

        private void EditJewelry_EditMaterial()
        {
            CurrentAppScreen = (int)ApplicationScreen.CreateMaterial;
        }

        private void EditJewelry_MaterialEdited()
        {
            CurrentAppScreen = (int)ApplicationScreen.EditJewelry;
        }


        private void Serialize(object p)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "Encoded XML file(*.xml)|*.xml|Encoded Binary data (*.bin)|*.bin|Encoded Text file(*.txt)|*.txt";
            var result = dialog.ShowDialog();

            if (result == null || result == false)
                return;

            var serialized = serializersFactory.NewSerializer(dialog.FileName).Serialize(new JewelrySerialized() { jewelries = JewelryList });

            Plugins[SelectedPluginIndex].Encode(serialized, dialog.FileName);
        }

        private void Deserialize(object p)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Any file(xml,bin,txt or their encoded versions)|*.*";

            var result = dialog.ShowDialog();

            if (result == null || result == false)
                return;

            var extension = Path.GetExtension(dialog.SafeFileName);

            var serializer = serializersFactory.NewSerializer(dialog.FileName);

            if(serializer == null)
            {
                MessageBox.Show("Cannot load this file.");
                return;
            }

            try
            {
                Object jewelries;

                if (SerializationHelper.IsPluginUsed(extension))
                {
                    var plugin = PluginFactory.GetPlugin(SerializationHelper.GetPluginExtension(Path.GetExtension(dialog.SafeFileName)), Plugins);

                    if (plugin == null)
                    {
                        MessageBox.Show("Some plugins are missing!");
                        return;
                    }

                    //get decoded file stream and its file name

                    var decodedFileName = plugin.Decode(new FileStream(dialog.FileName, FileMode.Open), dialog.FileName);
                     
                    jewelries = serializer.Deserialize(typeof(JewelrySerialized), new FileStream(decodedFileName, FileMode.Open));

                    //delete decoded file stream

                    File.Delete(decodedFileName);
                }
                else // no plugins were used
                {
                    jewelries = serializer.Deserialize(typeof(JewelrySerialized), new FileStream(dialog.FileName, FileMode.Open));
                }


                JewelryList = null;
                JewelryListUI = null;

                JewelryList = ((JewelrySerialized)jewelries).jewelries;
                JewelryListUI = new ObservableCollection<Jewelry>(JewelryList);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load this file. Either its contents are incorrect or was not generated by this program. Detailed message : {ex.Message}");
            }
        }

        private bool IsMaterialAlterred(Material material)
        {
            if (material.Name == string.Empty && material.Grams == 0 && material.PricePerGram == 0)
                return false;

            return true;
        }
    }






}
