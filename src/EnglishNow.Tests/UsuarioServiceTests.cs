using EnglishNow.Repositories;
using EnglishNow.Repositories.Entities;
using EnglishNow.Services;
using EnglishNow.Services.Enums;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNow.Tests
{
    [TestClass]
    public class UsuarioServiceTests
    {
        private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;

        private readonly IUsuarioService _usuarioService;

        public UsuarioServiceTests()
        {
            _usuarioRepositoryMock = new Mock<IUsuarioRepository>();

            _usuarioService = new UsuarioService(_usuarioRepositoryMock.Object);
        }

        [TestMethod]
        public void ValidarLogin_LoginValido_RetornaUsuario()
        {
            // Arrange
            var login = "administrador";
            var senha = "admin321";

            var usuario = new Usuario
            {
                Id = 1,
                Login = login,
                Senha = senha,
                PapelId = (int)Papel.Administrador
            };

            _usuarioRepositoryMock
                .Setup(c => c.ObterPorLogin(login))
                .Returns(usuario);

            // Act
            var result = _usuarioService.ValidarLogin(login, senha);

            // Assert
            _usuarioRepositoryMock.Verify(c => c.ObterPorLogin(login), Times.Once);

            Assert.IsTrue(
                result != null &&
                result.Sucesso == true &&
                result.Usuario != null &&
                result.Usuario.Login == login);
        }

        [TestMethod]
        public void ValidarLogin_SenhaInvalida_RetornaErro()
        {
            // Arrange
            var login = "administrador";
            var senha = "admin321";

            var usuario = new Usuario
            {
                Id = 1,
                Login = login,
                Senha = "admin123",
                PapelId = (int)Papel.Administrador
            };

            _usuarioRepositoryMock
                .Setup(c => c.ObterPorLogin(login))
                .Returns(usuario);

            // Act
            var result = _usuarioService.ValidarLogin(login, senha);

            // Assert
            _usuarioRepositoryMock.Verify(c => c.ObterPorLogin(login), Times.Once);

            Assert.IsTrue(
                result != null &&
                result.Sucesso == false &&
                result.MensagemErro == "senha incorreta!");
        }

        [TestMethod]
        public void ValidarLogin_LoginInvalido_RetornaErro()
        {
            // Arrange
            var login = "administrador";
            var senha = "admin321";

            _usuarioRepositoryMock
                .Setup(c => c.ObterPorLogin(login))
                .Returns((Usuario?)null);

            // Act
            var result = _usuarioService.ValidarLogin(login, senha);

            // Assert
            _usuarioRepositoryMock.Verify(c => c.ObterPorLogin(login), Times.Once);

            Assert.IsTrue(
                result != null &&
                result.Sucesso == false &&
                result.MensagemErro == "usuário não encontrado");
        }

        [TestMethod]
        public void ValidarLogin_LoginVazio_RetornaErro()
        {
            // Arrange
            var login = "";
            var senha = "admin321";

            // Act
            var result = _usuarioService.ValidarLogin(login, senha);

            // Assert
            _usuarioRepositoryMock.Verify(c => c.ObterPorLogin(login), Times.Never);

            Assert.IsTrue(
                result != null &&
                result.Sucesso == false &&
                result.Usuario == null &&
                result.MensagemErro == "Usuário é obrigatório");
        }

        [TestMethod]
        public void ValidarLogin_SenhaVazia_RetornaErro()
        {
            // Arrange
            var login = "administrador";
            var senha = "";

            // Act
            var result = _usuarioService.ValidarLogin(login, senha);

            // Assert
            _usuarioRepositoryMock.Verify(c => c.ObterPorLogin(login), Times.Never);

            Assert.IsTrue(
                result != null &&
                result.Sucesso == false &&
                result.Usuario == null &&
                result.MensagemErro == "Senha é obrigatória");
        }
    }
}
