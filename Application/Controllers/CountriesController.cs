using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Application.Data;
using Application.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Application.Controllers
{
    public class CountriesController : Controller
    {
        private readonly PersonDbContext _context;

        public CountriesController(PersonDbContext context)
        {
            _context = context;
        }

        // GET: Countries
        public async Task<IActionResult> Index(string search)
        {
            return View(await _context.Country.ToListAsync());
        }

        #region GETCountries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Countries>>> GetCountries(string search)
        {
            ViewData["SearchFilter"] = search;

            string connectionString = "Server=tcp:vikserver.database.windows.net,1433;Initial Catalog=vikdatabase;Persist Security Info=False;User ID=vikmcr;Password=*Fluzudo12;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionString);

            var countrieslist = new List<Countries>();

            countrieslist = _context.Country.FromSqlRaw($"EXEC GetCountry {search}").ToList();

            return View(countrieslist);
        }


        #endregion

        #region Details
        // GET: Countries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countries = await _context.Country
                .FirstOrDefaultAsync(m => m.Id == id);
            if (countries == null)
            {
                return NotFound();
            }

            return View(countries);
        }


        #endregion

        #region Create
        // GET: Country/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Country/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Countries country)
        {
            string connectionString = "Server=tcp:vikserver.database.windows.net,1433;Initial Catalog=vikdatabase;Persist Security Info=False;User ID=vikmcr;Password=*Fluzudo12;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string consult = "PostCountry";
                SqlCommand cmd = new SqlCommand(consult, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", country.Name);
                cmd.Parameters.AddWithValue("@FlagPhoto", country.FlagPhoto);

                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            return RedirectToAction(nameof(Index));
        }



        #endregion


        #region Edit
        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friend = await _context.Country.FindAsync(id);
            if (friend == null)
            {
                return NotFound();
            }
            return View(friend);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Countries country)
        {
            string connectionString = "Server=tcp:vikserver.database.windows.net,1433;Initial Catalog=vikdatabase;Persist Security Info=False;User ID=vikmcr;Password=*Fluzudo12;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string consult = "EditCountry";
                SqlCommand cmd = new SqlCommand(consult, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", country.Id);
                cmd.Parameters.AddWithValue("@Name", country.Name);
                cmd.Parameters.AddWithValue("@FlagPhoto", country.FlagPhoto);
                cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            return RedirectToAction(nameof(Index));
        }

        #endregion


        #region Delete
        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friend = await _context.Country
                .FirstOrDefaultAsync(m => m.Id == id);
            if (friend == null)
            {
                return NotFound();
            }

            return View(friend);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            string connectionString = "Server=tcp:vikserver.database.windows.net,1433;Initial Catalog=vikdatabase;Persist Security Info=False;User ID=vikmcr;Password=*Fluzudo12;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string consult = "DeleteCountry";
                SqlCommand cmd = new SqlCommand(consult, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion

        private bool CountriesExists(int id)
        {
            return _context.Country.Any(e => e.Id == id);
        }
    }
}

