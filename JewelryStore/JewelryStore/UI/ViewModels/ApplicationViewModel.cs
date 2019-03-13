using JewelryOop;
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

    /// <summary>
    /// ViewModel for storing information about current chat and messages in chat
    /// </summary>
    public class ApplicationViewModel : BaseViewModel {

        public ApplicationViewModel()
        {
            addMaterialsView = new AddMaterialsViewModel();
            editJewelryView = new EditJewelryViewModel(addMaterialsView, () => EditJewelry_MaterialEdited(), () => EditJewelry_EditMaterial());
            CurrentJewelryMaterials = new List<Material>();
            JewelryList = new List<Jewelry>();
            JewelryListUI = new ObservableCollection<Jewelry>();
            serializationProvider = new SerializationProvider();
        }

        public int CurrentAppScreen { get; set; }

        public string SelectedJewelryType { get; set; }

        public string JewelryName { get; set; }

        public string FoolRatioPercents { get; set; }

        public string[] Materials { get; set; } = new string[] { "Gemstone", "Premium Gemstone", "Premium Material" };

        public string ChosenMaterial { get; set; }

        public string[] JewelryTypes { get; set; } = new string[] { "Jewelry", "Bijouterie" };

        public ICommand AddJewelryCommand => new RelayCommand(() => AddJewelry(null), null);

        public ICommand AddMaterialCommand => new RelayCommand(() => AddMaterial(null), null);

        public ICommand EditJewelryCommand => new RelayCommand(() => EditJewelry(null), null);

        public ICommand GotoInitialPageCommand => new RelayCommand(() => GotoInitialPage(null), null);

        public ICommand DeleteJewelryCommand => new RelayCommand(() => DeleteJewelry(null), null);

        public ICommand SerializeCommand => new RelayCommand(() => Serialize(null), null);

        public ICommand DeserializeCommand => new RelayCommand(() => Deserialize(null), null);

        public ICommand TestCommand => new RelayCommand(() => Test(null), null);

        private Dictionary<string, Material> materialsAbstractNames { get; set; }

        private Dictionary<string, Jewelry> jewelryAbstractNames { get; set; }

        public AddMaterialsViewModel addMaterialsView { get; set; }

        public EditJewelryViewModel editJewelryView { get; set; }

        private List<Material> CurrentJewelryMaterials { get; set; }

        public List<Jewelry> JewelryList { get; set; }

        public ObservableCollection<Jewelry> JewelryListUI { get; set; }

        public SerializationProvider serializationProvider { get; set; }

        public string SelectedJewelry { get; set; }

        private void InitializeJewelry()
        {
            materialsAbstractNames = new Dictionary<string, Material>()
            {
                { "Gemstone", new Gemstone(string.Empty, 0, 0, Color.Black) },
                { "Premium Gemstone", new PremiumGemstone(string.Empty,0, 0, Color.Black, 1)},
                { "Premium Material", new PremiumMaterial(string.Empty, 0 , 0)  }
            };

            jewelryAbstractNames = new Dictionary<string, Jewelry>()
            {
                { "Jewelry", new Jewelry(string.Empty, null) },
                { "Bijouterie", new Bijouterie(string.Empty, null, 1)},
            };
        }

        private void AddMaterial(Object parameter)
        {
            try
            {
                InitializeJewelry();
                var materialToAdd = materialsAbstractNames[ChosenMaterial];
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

        private void Test(object p)
        {
            //Bijouterie bijouterie;
            //Jewelry anotherJewelry;
            //var list = new List<Material>();
            //list.Add(new Gemstone("gem", 12, 12, Color.Black));
            //list.Add(new PremiumMaterial("premMat", 12, 12));
            //list.Add(new Material("material", 12, 12));

            //bijouterie = new Bijouterie("1", list, 1);
            //anotherJewelry = new Jewelry("2", list);

            var fs = new FileStream("H:/test.txt", FileMode.Open);

            var x = new TextSerializer();
            x.Deserialize(fs);
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

                var jewelry = jewelryAbstractNames[SelectedJewelryType];

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
            dialog.Filter = "XML file(*.xml)|*.xml|Binary data (*.bin)|*.bin";
            var result = dialog.ShowDialog();

            if (result == null || result == false)
                return;

            var extension = Path.GetExtension(dialog.SafeFileName);

            serializationProvider.GetSerializer(extension).Serialize(JewelryList, new FileStream(dialog.FileName, FileMode.Create));
        }

        private void Deserialize(object p)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "XML file(*.xml)|*.xml|Binary data (*.bin)|*.bin";
            var result = dialog.ShowDialog();

            if (result == null || result == false)
                return;

            var extension = Path.GetExtension(dialog.SafeFileName);
            
            var jewelries = serializationProvider.GetSerializer(extension).Deserialize(new FileStream(dialog.FileName, FileMode.Open));

            JewelryList = null;
            JewelryListUI = null;

            JewelryList = jewelries;
            JewelryListUI = new ObservableCollection<Jewelry>(JewelryList);
        }

        private bool IsMaterialAlterred(Material material)
        {
            if (material.Name == string.Empty && material.Grams == 0 && material.PricePerGram == 0)
                return false;

            return true;
        }
    }






}
