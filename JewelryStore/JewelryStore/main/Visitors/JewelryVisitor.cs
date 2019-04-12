using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelryOop;
using JewelryStore.UI.ViewModels;

namespace JewelryStore.main.Visitors
{
    public class JewelryVisitor : IJewelryVisitor
    {
        private ApplicationViewModel viewModel { get; set; }
        public JewelryVisitor(ApplicationViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public void Visit(Jewelry jewelry)
        {
            jewelry.Name = viewModel.JewelryName;
            jewelry.Materials = new List<Material>(viewModel.CurrentJewelryMaterials);
        }
        public void Visit(Bijouterie bijouterie)
        {
            bijouterie.FoolRatio = double.Parse(viewModel.FoolRatioPercents) / 100;
        }
    }
}
