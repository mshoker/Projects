using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Models;

namespace CarDealership.Data.Interfaces
{
    public interface ISellerRepo
    {
        //list
        List<Seller> ListAll();
        //get
        Seller GetBy(int id);
        //add
        Seller Add(Seller seller);
        //edit
        Seller Edit(Seller seller);
    }
}
