using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MilibooAPI.Controllers;
using MilibooAPI.Models.EntityFramework;
using MilibooAPI.Models.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilibooAPI.Controllers.Tests
{
    [TestClass()]
    public class ProduitsControllerTests
    {
        private MilibooDBContext context;
        private ClientsController controller;
        private IDataRepository<Produit> dataRepository;

        private Mock<IDataRepository<Produit>> mockRepository;
        private ClientsController controllerMoq;


        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>().UseNpgsql();
            context = new MilibooDBContext();
            //controller = new UtilisateursController(context);
            /*dataRepository = new UtilisateurManager(context);
            controller = new UtilisateursController(dataRepository);

            mockRepository = new Mock<IDataRepository<Utilisateur>>();
            controllerMoq = new UtilisateursController(mockRepository.Object);*/
        }

        [TestMethod()]
        public void ProduitsControllerTest()
        {
            Assert.Fail();
        }
    }
}