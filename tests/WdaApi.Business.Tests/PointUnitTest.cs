using FluentValidation.TestHelper;
using System;
using WdaApi.Business.Models;
using WdaApi.Business.Models.Validations;
using Xunit;

namespace WdaApi.Business.Tests;

public class PointUnitTest
{
    private PointValidator validator;

    public PointUnitTest()
    {
        validator = new PointValidator();
    }

    [Fact(DisplayName = "Empty CityName error")]
    public void CreatePoint_WithValidParameters_EmptyCityName()
    {
        var model = new Point { CityName = "", };
        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.CityName);
    }

    [Fact(DisplayName = "Short CityName error")]
    public void CreatePoint_WithValidParameters_ShortCityName()
    {
        var model = new Point { CityName = "Fo" };
        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.CityName);
    }

    [Fact(DisplayName = "Long CityName error")]
    public void CreatePoint_WithValidParameters_LongCityName()
    {
        var model = new Point { CityName = "Fortalezaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" };
        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.CityName);
    }

    [Fact(DisplayName = "Long Latitude value error")]
    public void CreatePoint_WithValidParameters_LongLatitudeValue()
    {
        var model = new Point { Lat = 81 };
        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Lat);
    }

    [Fact(DisplayName = "Short Latitude value error")]
    public void CreatePoint_WithValidParameters_ShortLatitudeValue()
    {
        var model = new Point { Lat = -81 };
        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Lat);
    }

    [Fact(DisplayName = "Long Longitude value error")]
    public void CreatePoint_WithValidParameters_LongLongitudeValue()
    {
        var model = new Point { Lng = 181 };
        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Lng);
    }

    [Fact(DisplayName = "Short Longitude value error")]
    public void CreatePoint_WithValidParameters_ShortLongitudeValue()
    {
        var model = new Point { Lng = -181 };
        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Lng);
    }

    [Fact(DisplayName = "DateInit greater than DateEnd point")]
    public void CreatePoint_WithValidParameters_ValidateDateInit()
    {
        var model = new Point { DateInit = DateTime.Parse("2022-05-15"), DateEnd = DateTime.Parse("2022-05-14") };
        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.DateInit);
    }

    [Fact(DisplayName = "DateInit less than DateEnd point")]
    public void CreatePoint_WithValidParameters_ValidateDateEnd()
    {
        var model = new Point { DateEnd = DateTime.Parse("2022-05-16"), DateInit = DateTime.Parse("2022-05-17") };
        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.DateEnd);
    }
}