using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BancosProject.Business.Implementacion;
using BancosProject.Business.Interfaz;

namespace BancosProject.UnitTest
{
    /// <summary>
    /// Esta es una prueba de encripciòn del archivo
    /// </summary>
    [TestClass]
    public class EncryptTest
    {
        ICustomCriptography _cifrado;

        public EncryptTest()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
            _cifrado = new Cifrado(2048);
            
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Obtiene o establece el contexto de las pruebas que proporciona
        ///información y funcionalidad para la serie de pruebas actual.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Atributos de prueba adicionales
        //
        // Puede usar los siguientes atributos adicionales conforme escribe las pruebas:
        //
        // Use ClassInitialize para ejecutar el código antes de ejecutar la primera prueba en la clase
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup para ejecutar el código una vez ejecutadas todas las pruebas en una clase
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Usar TestInitialize para ejecutar el código antes de ejecutar cada prueba 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup para ejecutar el código una vez ejecutadas todas las pruebas
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void CifrarArchivo()
        {
            string publicKey = @"F:\Universidad\Teoría de la Información y la Comunicación\Project\FTP\public.xml";
            string filePath = @"F:\Universidad\Teoría de la Información y la Comunicación\Project\FTP\clientes.txt";
            string filePathDestiny = @"F:\Universidad\Teoría de la Información y la Comunicación\Project\FTP\clientesEncripted.txt";
            _cifrado.Crypt(publicKey, filePath, filePathDestiny);
        }

        [TestMethod]
        public void DecifrarArchivo()
        {
            string privateKey = @"F:\Universidad\Teoría de la Información y la Comunicación\Project\FTP\private.xml";
            string filePath = @"F:\Universidad\Teoría de la Información y la Comunicación\Project\FTP\clientesEncripted.txt";
            string filePathDestiny = @"F:\Universidad\Teoría de la Información y la Comunicación\Project\FTP\clientesDecripted.txt";
            _cifrado.Decrypt(privateKey, filePath, filePathDestiny);
        }

        [TestMethod]
        public void GenerarLlaves()
        {
            string filePath = @"F:\Universidad\Teoría de la Información y la Comunicación\Project\FTP";
            _cifrado.GenerateKeys(filePath);
        }
    }
}
