using FluentValidation.TestHelper;
using System;
using WdaApi.Business.Models;
using WdaApi.Business.Models.Validations;
using Xunit;

namespace WdaApi.Business.Tests;

public class FreightUnitTest
{
    private FreightValidator validator;

    public FreightUnitTest()
    {
        validator = new FreightValidator();
    }

    [Fact(DisplayName = "Empty Name error")]
    public void CreateFreight_WithValidParameters_EmptyName()
    {
        var model = new Freight {Name=""};
        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Name);   
    }

    [Fact(DisplayName = "Empty IsTrackedTruck error")]
    public void CreateFreight_WithValidParameters_EmptyIsTrackedTruck()
    {
        var model = new Freight { IsTrackedTruck = "" };
        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.IsTrackedTruck);
    }

    [Fact(DisplayName = "Empty IsSafeTruckk error")]
    public void CreateFreight_WithValidParameters_EmptyIsSafeTruck()
    {
        var model = new Freight { IsSafeTruck = "" };
        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.IsSafeTruck);
    }

    [Fact(DisplayName = "Empty Observations error")]
    public void CreateFreight_WithValidParameters_EmptyObservations()
    {
        var model = new Freight { Observations = "" };
        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Observations);
    }

    [Fact(DisplayName = "Long Observations error")]
    public void CreateFreight_WithValidParameters_LongObservations()
    {
        var model = new Freight { Observations = "InformacaooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooaaaaaaaaaaaaaaaaaaaaaaInformacaooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooaaaaaaaaaaaaaaaaaa" };
        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Observations);
    }

    [Fact(DisplayName = "Long Name error")]
    public void CreateFreight_WithValidParameters_LongName()
    {
        var model = new Freight { Name = "FortalezaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaFortalezaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" };
        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact(DisplayName = "Short Name error")]
    public void CreateFreight_WithValidParameters_ShortName()
    {
        var model = new Freight { Name = "Fo" };
        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact(DisplayName = "Short Weight value error")]
    public void CreateFreight_WithValidParameters_LenghtWeight()
    {
        var model = new Freight { Weight = -1 };
        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Weight);
    }

    [Fact(DisplayName = "Short StatusId value error ")]
    public void CreateFreight_WithValidParameters_LenghtStatusId()
    {
        var model = new Freight { StatusId = 1 };
        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Weight);
    }

    [Fact(DisplayName = "DateInit greater than DateEnd point")]
    public void CreateFreight_WithValidParameters_ValidateDateInit()
    {
        var model = new Freight { DateInit = DateTime.Parse("2022-05-15"), DateEnd = DateTime.Parse("2022-05-14") };
        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.DateInit);
    }

    [Fact(DisplayName = "DateInit less than DateEnd point")]
    public void CreateFreight_WithValidParameters_ValidateDateEnd()
    {
        var model = new Freight { DateEnd = DateTime.Parse("2022-05-16"), DateInit = DateTime.Parse("2022-05-17") };
        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.DateEnd);
    }
}