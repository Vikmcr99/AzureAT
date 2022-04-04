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
    public class PeopleController : Controller
    {
        private readonly PersonDbContext _context;

        public PeopleController(PersonDbContext context)
        {
            _context = context;
        }

        // GET: Friends
        public async Task<IActionResult> Index(string search)
        {
            return View(await _context.Persons.ToListAsync());
        }

        #region GETFriends
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> Getfriends(string search)
        {
            ViewData["SearchFilter"] = search;

            string connectionString = "Server=tcp:vikserver.database.windows.net,1433;Initial Catalog=vikdatabase;Persist Security Info=False;User ID=vikmcr;Password=*Fluzudo12;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionString);

            var Friendlist = new List<Person>();

            Friendlist = _context.Persons.FromSqlRaw($"EXEC GetFriend {search}").ToList();

            return View(Friendlist);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Person>>> GetFriends(int id)
        {

            string connectionString = "Server=tcp:vikserver.database.windows.net,1433;Initial Catalog=vikdatabase;Persist Security Info=False;User ID=vikmcr;Password=*Fluzudo12;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string consult = "GetFriend";
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

            return View(await _context.Persons.ToListAsync());

        }


        #endregion

        #region Create
        // GET: Friends/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Friends/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Person person)
        {
            string connectionString = "Server=tcp:vikserver.database.windows.net,1433;Initial Catalog=vikdatabase;Persist Security Info=False;User ID=vikmcr;Password=*Fluzudo12;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string consult = "PostFriend";
                SqlCommand cmd = new SqlCommand(consult, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", person.Name);
                cmd.Parameters.AddWithValue("@Lastname", person.Lastname);
                cmd.Parameters.AddWithValue("@Telphone", person.Telphone);
                cmd.Parameters.AddWithValue("@Email", person.Email);
                cmd.Parameters.AddWithValue("@Birthday", person.Birthday);
                cmd.Parameters.AddWithValue("@Country", person.Country);
                cmd.Parameters.AddWithValue("@Photo", person.Photo);
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


        #region Details
        // GET: Friends/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friend = await _context.Persons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (friend == null)
            {
                return NotFound();
            }

            return View(friend);
        }

        #endregion


        #region Edit
        // GET: Friends/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friend = await _context.Persons.FindAsync(id);
            if (friend == null)
            {
                return NotFound();
            }
            return View(friend);
        }

        // POST: Friends/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Person person)
        {
            string connectionString = "Server=tcp:vikserver.database.windows.net,1433;Initial Catalog=vikdatabase;Persist Security Info=False;User ID=vikmcr;Password=*Fluzudo12;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string consult = "EditFriend";
                SqlCommand cmd = new SqlCommand(consult, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", person.Id);
                cmd.Parameters.AddWithValue("@Name", person.Name);
                cmd.Parameters.AddWithValue("@Lastname", person.Lastname);
                cmd.Parameters.AddWithValue("@Telphone", person.Telphone);
                cmd.Parameters.AddWithValue("@Email", person.Email);
                cmd.Parameters.AddWithValue("@Birthday", person.Birthday);
                cmd.Parameters.AddWithValue("@Country", person.Country);
                cmd.Parameters.AddWithValue("@Photo", person.Photo);
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
        // GET: Friends/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friend = await _context.Persons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (friend == null)
            {
                return NotFound();
            }

            return View(friend);
        }

        // POST: Friends/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            string connectionString = "Server=tcp:vikserver.database.windows.net,1433;Initial Catalog=vikdatabase;Persist Security Info=False;User ID=vikmcr;Password=*Fluzudo12;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string consult = "DeleteFriend";
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
        private bool FriendExists(int id)
        {
            return _context.Persons.Any(e => e.Id == id);
        }

        
    }
}
