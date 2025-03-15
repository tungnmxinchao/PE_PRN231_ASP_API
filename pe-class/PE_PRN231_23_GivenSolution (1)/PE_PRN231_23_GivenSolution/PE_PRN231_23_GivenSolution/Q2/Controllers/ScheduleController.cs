using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Nodes;
using GivenAPIs.Models;
using Microsoft.AspNetCore.Mvc;
using Q2.DTOs;

namespace Q2.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly HttpClient _httpClient = null;
        private string baseUrl = "";

        public ScheduleController()
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);

            baseUrl = "http://localhost:5100/api";
        }

        public async Task<IActionResult> Index(string selectedDate)
        {
            var rooms = await FindAllRoom();
            var timeSlots = await FindAllTimeSlot();
            var movies = await FindAllMovie();

            if (!string.IsNullOrEmpty(selectedDate))
            {
                ViewBag.selectedDate = DateTime.Parse(selectedDate).ToString("MM/dd/yyyy"); // Lưu vào ViewBag để hiển thị
            }
            else
            {
                ViewBag.SelectedDate = "10-24-2023";
                selectedDate = "10-24-2023";
            }
            var schedules = await FindAllSchedule(rooms, timeSlots, movies, selectedDate);

            return View(schedules);
        }

        private async Task<List<ScheduleViewModel>> FindAllSchedule(List<Room> rooms, List<TimeSlot> timeSlots, List<Movie> movies, string selectedDate)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{baseUrl}/Schedule/List/{selectedDate}");
            if (!response.IsSuccessStatusCode)
            {
                return new List<ScheduleViewModel>();
            }

            string data = await response.Content.ReadAsStringAsync();
            using JsonDocument doc = JsonDocument.Parse(data);
            JsonElement root = doc.RootElement;

            return root.EnumerateArray()
                .Select(x => new ScheduleViewModel
                {
                    Id = x.GetProperty("id").GetInt32(),
                    MovieName = movies.FirstOrDefault(m => m.Id == x.GetProperty("movieId").GetInt32())?.Title ?? "Unknown",
                    RoomTitle = rooms.FirstOrDefault(r => r.Id == x.GetProperty("roomId").GetInt32())?.Title ?? "Unknown",
                    TimeSlotName = timeSlots.FirstOrDefault(t => t.Id == x.GetProperty("timeSlotId").GetInt32()) != null
                        ? $"{timeSlots.FirstOrDefault(t => t.Id == x.GetProperty("timeSlotId").GetInt32()).StartTime} - {timeSlots.FirstOrDefault(t => t.Id == x.GetProperty("timeSlotId").GetInt32()).EndTime}"
                        : "Unknown",
                    StartDate = DateTime.Parse(x.GetProperty("startDate").GetString()),
                    EndDate = DateTime.Parse(x.GetProperty("endDate").GetString()),
                    Note = x.TryGetProperty("note", out var note) ? note.GetString() : null
                }).ToList();
        }

        private async Task<List<TimeSlot>> FindAllTimeSlot()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(baseUrl + "/TimeSlot/List");
            string data = await response.Content.ReadAsStringAsync();

            using JsonDocument doc = JsonDocument.Parse(data);

            JsonElement root = doc.RootElement;

            var items = root.EnumerateArray()
                .Select(x => new TimeSlot
                {
                    Id = x.GetProperty("id").GetInt32(),
                    StartTime = TimeSpan.Parse(x.GetProperty("startTime").GetString()),
                    EndTime = TimeSpan.Parse(x.GetProperty("endTime").GetString())
                }).ToList();

            return items;
        }

        private async Task<List<Room>> FindAllRoom()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(baseUrl + "/Room/List");
            string data = await response.Content.ReadAsStringAsync();

            using JsonDocument doc = JsonDocument.Parse(data);

            JsonElement root = doc.RootElement;

            var items = root.EnumerateArray()
                .Select(x => new Room
                {
                    Id = x.GetProperty("id").GetInt32(),
                    Title = x.GetProperty("title").GetString(),
                    Capacity = x.GetProperty("capacity").GetInt32()
                }).ToList();

            return items;
        }

        private async Task<List<Movie>> FindAllMovie()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(baseUrl + "/Movie/List");
            string data = await response.Content.ReadAsStringAsync();

            using JsonDocument doc = JsonDocument.Parse(data);

            JsonElement root = doc.RootElement;

            var items = root.EnumerateArray()
                .Select(x => new Movie
                {
                    Id = x.GetProperty("id").GetInt32(),
                    Title = x.GetProperty("title").GetString(),
                }).ToList();

            return items;
        }
    }
}
