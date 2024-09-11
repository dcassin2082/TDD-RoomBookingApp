using BookingApp.Core.DataServices;
using BookingApp.Core.DataServices.Domain;
using BookingApp.Core.Enums;
using BookingApp.Core.Models;
using BookingApp.Core.Processors;
using Moq;
using Shouldly;
using System.Runtime.CompilerServices;

namespace BookingApp.Core
{
    public class BookingRequestProcessorTest
    {
        private IBookingRequestProcessor _processor;
        private BookingRequest _request;
        private Mock<IBookingService> _bookingServiceMock;
        private List<Room> _availableRooms;

        public BookingRequestProcessorTest()
        {
            // Arrange
            _request = new BookingRequest
            {
                FullName = "Test Name",
                Email = "test@request.com",
                Date = new DateTime(2024, 09, 11)
            };

            _availableRooms = new List<Room>() { new Room() { Id = 1 } };

            _bookingServiceMock = new Mock<IBookingService>();
            _bookingServiceMock.Setup(q => q.GetAvailableRooms(_request.Date))
                .Returns(_availableRooms);

            _processor = new BookingRequestProcessor(_bookingServiceMock.Object);
        }

        [Fact]
        public void ShouldReturnBookingResponseWithRequestValues()
        {
            // Act
            BookingResult result = _processor.BookRoom(_request);

            // Assert
            Assert.NotNull(result); // xUnit
            result.ShouldNotBeNull(); // Shouldly

            Assert.Equal(_request.FullName, result.FullName);
            Assert.Equal(_request.Email, result.Email);
            Assert.Equal(_request.Date, result.Date);

            result.FullName.ShouldBe(_request.FullName);
            result.Email.ShouldBe(_request.Email);
            result.Date.ShouldBe(_request.Date);

        }

        [Fact]
        public void ShouldThrowExceptionForNullRequest()
        {

            // assert
            var exception = Should.Throw<ArgumentNullException>(() => _processor.BookRoom(null));
            exception.ParamName.ShouldBe("bookingRequest");

            Assert.Throws<ArgumentNullException>(() => _processor.BookRoom(null));

        }

        [Fact]
        public void ShouldSaveRoomBookingRequest()
        {
            // Arrange
            Booking savedBooking = null;
            _bookingServiceMock.Setup(q => q.Save(It.IsAny<Booking>()))
                .Callback<Booking>(booking =>
                {
                    savedBooking = booking;
                });

            // Act
            _processor.BookRoom(_request);

            // Assert
            _bookingServiceMock.Verify(q => q.Save(It.IsAny<Booking>()), Times.Once);

            savedBooking.ShouldNotBeNull();
            savedBooking.FullName.ShouldBe(_request.FullName);
            savedBooking.Email.ShouldBe(_request.Email);
            savedBooking.Date.ShouldBe(_request.Date);
            savedBooking.RoomId.ShouldBe(_availableRooms.First().Id);

            Assert.NotNull(savedBooking);
            Assert.Equal(_request.FullName, savedBooking.FullName);
            Assert.Equal(_request.Email, savedBooking.Email);
            Assert.Equal(_request.Date, savedBooking.Date);
            Assert.Equal(_availableRooms.First().Id, savedBooking.RoomId);
        }

        [Fact]
        public void ShouldNotSaveBookingRequestIfNoneAvailable()
        {
            _availableRooms.Clear();
            _processor.BookRoom(_request);
            _bookingServiceMock.Verify(q => q.Save(It.IsAny<Booking>()), Times.Never);
        }

        [Theory]
        [InlineData(BookingResultFlag.Failure, false)]
        [InlineData(BookingResultFlag.Success, true)]
        public void ShouldReturnSuccessFailureFlagInResult(BookingResultFlag bookingSuccessFlag, bool isAvailable)
        {
            if(!isAvailable)
            {
                _availableRooms.Clear();
            }

            var result = _processor.BookRoom(_request);

            bookingSuccessFlag.ShouldBe(result.Flag);
            Assert.Equal(bookingSuccessFlag, result.Flag);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(null, false)]
        public void ShouldReturnBookingIdInResult(int? roomBookingId, bool isAvailable)
        {
            if(!isAvailable)
            {
                _availableRooms.Clear();
            }
            else
            {
                _bookingServiceMock.Setup(q => q.Save(It.IsAny<Booking>()))
              .Callback<Booking>(booking =>
              {
                  booking.Id = roomBookingId;
              });
            }

            var result = _processor.BookRoom(_request);
            result.RoomBookingId.ShouldBe(roomBookingId);
            Assert.Equal(roomBookingId, result.RoomBookingId);
        }
    }
}
