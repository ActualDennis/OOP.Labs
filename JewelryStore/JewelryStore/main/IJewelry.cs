using JewelryStore.main.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryStore.main
{
    public interface IJewelry
    {
        void Accept(IJewelryVisitor visitor);
    }
}
