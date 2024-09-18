using Moq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PassportChecker.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AutoMapper;

namespace PassportChecker.Tests;
/*
public class ReaderServiceTests
{
    [Fact]
    public void ReaderService_Reads()
    {
        //Arrange
        var mockDb = new Mock<ApiDbContext>();
        var mockMapper= new Mock<IMapper>();
        var x = new ReaderService(mockDb.Object, mockMapper.Object);

        var mockTable = new Mock<ITableService>();
        BotService botService = new BotService(mockTable.Object);
        Table table = new Table();
        Coordinate coordinate = new Coordinate();
        coordinate.X = 1;
        coordinate.Y = 2;
        //Act
        var result = botService.Shoot(table, coordinate);
        //Assert
        Assert.True(result != Models.Enums.ShootResult.SamePointShooted);
    }
    
}
*/