using Moq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PassportChecker.Services.Implementations;
using PassportChecker.Models.DbModels;
using NUnit.Framework.Legacy;
using Microsoft.AspNetCore.Routing;

namespace PassportChecker.Tests;

[TestFixture]
public class DataConfiguratorTests
{
    [Test]
    public void Test()
    {
        ClassicAssert.IsTrue(true);
    }

    [Test]
    public void GetFirstExceptSecond_Correct_HasErrorFalse()
    {
        //Arrange
        List<PassportModel> first = new List<PassportModel>()
        {
            new PassportModel(){ Series = 1111, Number = 111111},
            new PassportModel(){ Series = 2222, Number = 222222},
            new PassportModel(){ Series = 3333, Number = 333333},
            new PassportModel(){ Series = 5555, Number = 555555},
            new PassportModel(){ Series = 7777, Number = 777777}
        };
        List<PassportModel> second = new List<PassportModel>()
        {
            new PassportModel(){ Series = 5555, Number = 555555},
            new PassportModel(){ Series = 6666, Number = 666666},
            new PassportModel(){ Series = 7777, Number = 777777}
        };
        List<PassportModel> CorrectPassports = new List<PassportModel>()
        {
            new PassportModel(){ Series = 1111, Number = 111111},
            new PassportModel(){ Series = 2222, Number = 222222},
            new PassportModel(){ Series = 3333, Number = 333333}
        };

        //Act
        var result = DataConfigurator.GetFirstExceptSecond(first, second);

        bool NotFound = false;
        foreach (var model1 in result)
        {
            NotFound = true;
            foreach (var model2 in CorrectPassports)
            {
                if (model1.Equals(model2))
                {
                    NotFound = false;
                    break;
                }
            }
            if (NotFound == false) break;
        }

        //Assert
        ClassicAssert.IsFalse(NotFound);

    }

    [Test]
    public void GetChanges_Scenario_Result()
    {
        //Arrange
        List<PassportModel> needToDelete = new List<PassportModel>()
        {
            new PassportModel(){ Series = 1111, Number = 111111},
            new PassportModel(){ Series = 2222, Number = 222222}
        };
        List<PassportModel> needToAdd = new List<PassportModel>()
        {
            new PassportModel(){ Series = 3333, Number = 444444},
            new PassportModel(){ Series = 5555, Number = 555555}
        };
        List<ChangeModel> CorrectChanges = new List<ChangeModel>()
        {
            new ChangeModel(){ Series = 1111, Number = 111111, IsActive = true},
            new ChangeModel(){ Series = 2222, Number = 222222, IsActive = true},
            new ChangeModel(){ Series = 3333, Number = 444444, IsActive = false},
            new ChangeModel(){ Series = 5555, Number = 555555, IsActive = false},
        };

        //Act
        var result = DataConfigurator.GetChanges(needToDelete, needToAdd);

        bool Incorrect = false;
        for (int i = 0; i < result.Count; i++)
        {
            if (result[i].Series == CorrectChanges[i].Series && result[i].Number == CorrectChanges[i].Number)
            {
                if ((i < 2 && result[i].IsActive == false) || (i > 1 && result[i].IsActive == true)) Incorrect = true;
            }
        }

        //Assert
        ClassicAssert.IsFalse(Incorrect);
    }

}
