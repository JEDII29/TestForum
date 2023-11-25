using System;
using Xunit;
using TestForum.API.Controllers;
using TestForum.API.Services;
using Microsoft.AspNetCore.Identity;
using TestForum.Data.Entities;
using Moq;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using TestForum.API.Abstract;
using TestForum.API.Models;

namespace TestForum.tests
{
	public class UnitTest1
	{
		[Fact]
		public async Task GenerateAccessToken_ReturnsValidToken()
		{
			var userStoreMock = new Mock<IUserStore<UserEntity>>();
			// Arrange
			var user = new UserEntity
			{
				Id = Guid.NewGuid(),
				UserName = "testuser"
				// Dodaj inne właściwości użytkownika, jeśli wymagane
			};

			var userManagerMock = new Mock<UserManager<UserEntity>>(
					userStoreMock.Object,
					null,
					null,
					null,
					null,
					null,
					null,
					null,
					null);
			userManagerMock.Setup(service => service.GetRolesAsync(user))
							.ReturnsAsync(new List<string> { "user" });

			var authenticationService = new AuthenticationService(userManagerMock.Object);

			// Act
			var token = await authenticationService.GenerateAccessToken(user);

			// Assert
			Assert.NotNull(token);

			// Tutaj możesz przeprowadzić bardziej szczegółowe sprawdzenie struktury i zawartości tokena.
			// Na przykład, możesz użyć odpowiednich narzędzi do analizy JWT i zweryfikować zawartość.

			// Przykładowa weryfikacja dla xUnit:
			var handler = new JsonWebTokenHandler();
			var validationResult = handler.ValidateToken(token, new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidIssuer = "http://localhost:5063",
				ValidateAudience = false,
				ValidateLifetime = true,
				ClockSkew = TimeSpan.Zero,
				RequireExpirationTime = true,
				IssuerSigningKey = new RsaSecurityKey(RSA.Create())
			});

			Assert.True(validationResult.IsValid);
		}

		[Fact]
		public async Task GetAllUsers_ReturnsListOfUsers()
		{
			// Arrange
			var users = new List<UserDTO>
		{
			new UserDTO { Id = Guid.NewGuid(), UserName = "user1" },
			new UserDTO { Id = Guid.NewGuid(), UserName = "user2" },
				// Dodaj więcej użytkowników w razie potrzeby
		};
			var userServiceMock = new Mock<IUsersService>(); // Załóżmy, że masz oddzielną usługę do obsługi operacji na użytkownikach
			userServiceMock.Setup(service => service.GetAllUsers())
						   .Returns(Task.FromResult<IEnumerable<UserDTO>>(users.AsEnumerable()));

			var controller = new UserController(userServiceMock.Object);

			// Act
			var result = await controller.GetAll();

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			var model = Assert.IsType<List<UserDTO>>(okResult.Value);

			Assert.Equal(users.Count, model.Count);
			// Dodaj inne asercje zgodnie z oczekiwaniami dla Twojego przypadku użycia.
		}
	}
}