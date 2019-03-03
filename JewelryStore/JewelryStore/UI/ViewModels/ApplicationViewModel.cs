using JewelryOop;
using JewelryStore.UI.Data;
using JewelryStore.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace JewelryStore.UI.ViewModels {

    /// <summary>
    /// ViewModel for storing information about current chat and messages in chat
    /// </summary>
    public class ApplicationViewModel : BaseViewModel {

        public ApplicationViewModel()
        {
            addMaterialsView = new AddMaterialsViewModel();
            CurrentJewelryMaterials = new List<Material>();
            JewelryList = new List<Jewelry>();
        }

        private static readonly object padlock = new object();

        private static ApplicationViewModel _instance;

        public static ApplicationViewModel Instance
        {
            get
            {
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new ApplicationViewModel();
                    }
                    return _instance;
                }
            }
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

        public  ICommand EditJewelryCommand => new RelayCommand(() => Edit(null), null);

        public ICommand ViewJewelry => new RelayCommand(() => View(null), null);

        private Dictionary<string, Material> materialsAbstractNames { get; set; }

        private Dictionary<string, Jewelry> jewelryAbstractNames { get; set; }

        public AddMaterialsViewModel addMaterialsView { get; set; }

        private List<Material> CurrentJewelryMaterials { get; set; }

        private List<Jewelry> JewelryList { get; set; }

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
                addMaterialsView.AlterMaterial(ref materialToAdd,() => MaterialAddedCallback());
                CurrentAppScreen = (int)ApplicationScreen.CreateMaterial;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Could not add material. Reason: {ex.Message}");
            }
        }

        private void View(Object parameter)
        {

        }

        private void Edit(Object parameter)
        {

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

                if (JewelryName == string.Empty)
                {
                    MessageBox.Show("Please choose some name for your jewelry.");
                    return;
                }

                var jewelry = jewelryAbstractNames[SelectedJewelryType];

                jewelry.Name = JewelryName;
                jewelry.Materials = CurrentJewelryMaterials;

                if (jewelry is Bijouterie)
                {
                    ((Bijouterie)jewelry).FoolRatio = double.Parse(FoolRatioPercents) / 100;
                }

                MessageBox.Show($"Successfully added {jewelry.Name}.");

                JewelryList.Add(jewelry);

                CurrentJewelryMaterials = null;
                CurrentJewelryMaterials = new List<Material>();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Can't add jewelry. Reason: {ex.Message}.");
            }
        }

        private void MaterialAddedCallback()
        {
            CurrentAppScreen = (int)ApplicationScreen.Initial;
        }

        private bool IsMaterialAlterred(Material material)
        {
            if (material.Name == string.Empty && material.Grams == 0 && material.PricePerGram == 0)
                return true;

            return false;
        }
    }






}
