using DAL_Factory;
using Interfaces;

﻿namespace BLL.Models;

public class Customer(Factory.ServiceType serviceType) : Domain.Models.Customer
{
    private readonly IDal _service = Factory.GetService(serviceType);
    
}