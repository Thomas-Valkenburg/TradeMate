﻿using DAL_Factory;
using Interfaces;

namespace BLL.Models;

public class Customer(Factory.ServiceType serviceType) : Domain.Models.Customer
{
    private readonly IDal _service = Factory.GetService(serviceType);

    public Result ChangeName(string name)
    {
        Name = name;
        
        return SaveCustomer();
    }

    public Result ChangeEmail(string email)
    {
        Email = email;
        
        return SaveCustomer();
    }
    
    public Result SaveCustomer()
    {
        return _service.CreateCustomer(this);
    }

    public List<Inventory> GetInventories()
    {
        var list = new List<Inventory>();

        _service.GetAllInventories(Id).ForEach(inventory =>
        {
            list.Add(Models.Inventory.ConvertToBll(inventory, _service));
        });

        return list;
    }

    public Result AddInventory(string name)
    {
        var inventory = new Inventory(_service)
        {
            Name = name,
            Customer = this
        };

        var result = Inventory.CheckIfValid(inventory);
        if (!result.Success) return result;

        return _service.CreateInventory(inventory);
    }
}