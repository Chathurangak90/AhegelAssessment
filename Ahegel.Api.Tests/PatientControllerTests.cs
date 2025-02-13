using Ahegel.Contracts;
using Ahegel.Entities;
using Ahegel.Api;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Ahegel.Tests
{
    public class PatientControllerTests
    {
        private readonly Mock<IPatientService> _mockPatientService;
        private readonly PatientController _controller;

        public PatientControllerTests()
        {
            _mockPatientService = new Mock<IPatientService>();
            _controller = new PatientController(_mockPatientService.Object);
        }

        [Fact]
        public async Task GetPatients_ReturnsOk_WithPatients()
        {
            // Arrange
            var patients = new List<Patient> { new Patient { Id = 1, Name = "Chathuranga" } };
            _mockPatientService.Setup(service => service.GetPatients()).ReturnsAsync(patients);

            // Act
            var result = await _controller.GetPatientById();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(patients, okResult.Value);
        }

        [Fact]
        public async Task GetPatient_ReturnsOk_WithPatient()
        {
            // Arrange
            var patient = new Patient { Id = 1, Name = "Chathuranga" };
            _mockPatientService.Setup(service => service.GetPatientById(1)).ReturnsAsync(patient);

            // Act
            var result = await _controller.GetPatientById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(patient, okResult.Value);
        }

        [Fact]
        public async Task GetPatient_ReturnsNotFound_WhenPatientDoesNotExist()
        {
            // Arrange
            _mockPatientService.Setup(service => service.GetPatientById(1)).ReturnsAsync((Patient)null);

            // Act
            var result = await _controller.GetPatientById(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task CreatePatient_ReturnsCreatedAtAction_WithPatient()
        {
            // Arrange
            var patient = new Patient { Id = 1, Name = "Chathuranga" };
            _mockPatientService.Setup(service => service.CreatePatient(It.IsAny<Patient>())).ReturnsAsync(patient);

            // Act
            var result = await _controller.CreatePatient(patient);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(patient, createdAtActionResult.Value);
        }

        [Fact]
        public async Task UpdatePatient_ReturnsOk_WithUpdatedPatient()
        {
            // Arrange
            var patient = new Patient { Id = 1, Name = "Chathuranga" };
            _mockPatientService.Setup(service => service.UpdatePatient(1, It.IsAny<Patient>())).ReturnsAsync(patient);

            // Act
            var result = await _controller.UpdatePatient(1, patient);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(patient, okResult.Value);
        }

        [Fact]
        public async Task UpdatePatient_ReturnsNotFound_WhenPatientDoesNotExist()
        {
            // Arrange
            _mockPatientService.Setup(service => service.UpdatePatient(1, It.IsAny<Patient>())).ReturnsAsync((Patient)null);

            // Act
            var result = await _controller.UpdatePatient(1, new Patient());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task SoftDeletePatient_ReturnsOk_WhenSuccessful()
        {
            // Arrange
            _mockPatientService.Setup(service => service.SoftDeletePatient(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.SoftDeletePatient(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SoftDeletePatient_ReturnsNotFound_WhenPatientDoesNotExist()
        {
            // Arrange
            _mockPatientService.Setup(service => service.SoftDeletePatient(1)).ReturnsAsync(false);

            // Act
            var result = await _controller.SoftDeletePatient(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
