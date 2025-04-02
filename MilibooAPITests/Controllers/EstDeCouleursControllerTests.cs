using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MilibooAPI.Controllers;
using MilibooAPI.Models.DataManager;
using MilibooAPI.Models.EntityFramework;
using MilibooAPI.Models.Repository;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilibooAPI.Controllers.Tests
{
    [TestClass()]
    public class EstDeCouleursControllerTests
    {
        private MilibooDBContext context;
        private EstDeCouleursController controller;
        private IDataRepositoryEstDeCouleur dataRepository;

        private Mock<IDataRepositoryEstDeCouleur> mockRepository;
        private Mock<MilibooDBContext> mockRepositoryMiliboo;
        private EstDeCouleursController controllerMoq;

        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>().UseNpgsql();
            context = new MilibooDBContext();

            dataRepository = new EstDeCouleurManager(context);
            controller = new EstDeCouleursController(dataRepository,context);

            mockRepository = new Mock<IDataRepositoryEstDeCouleur>();
            mockRepositoryMiliboo = new Mock<MilibooDBContext>();
            controllerMoq = new EstDeCouleursController(mockRepository.Object, mockRepositoryMiliboo.Object);
        }

        [TestMethod()]
        public void EstDeCouleursControllerTest()
        {
            // Assert
            Assert.IsNotNull(context, "Le contexte est nul.");
            Assert.IsNotNull(controller, "Le controller est nul.");
        }

        [TestMethod]
        public async Task GetAllEstDeCouleursWithPhotos_ShouldReturnOkResult()
        {
            
        }

    }
}