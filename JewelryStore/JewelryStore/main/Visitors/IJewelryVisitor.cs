using JewelryOop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryStore.main.Visitors
{
    public interface IJewelryVisitor
    {
        void Visit(Jewelry jewelry);
        void Visit(Bijouterie bijouterie);
    } 
}
