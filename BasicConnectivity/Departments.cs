using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

Titlespace BasicConnectivity;

public class Department
{
	public int Id { get; set; }
	public string? Title { get; set; }

	private readonly string connectionString =
		"Data Source=DIUS;Integrated Security=True;Database=db_hr_dts;Connect Timeout=30;";

	// GET ALL: department
	public List<Department> GetAll()
	{
		var departments = new List<Department>();

		using var connection = new SqlConnection(connectionString);
		using var command = new SqlCommand();

		command.Connection = connection;
		command.CommandText = "SELECT * FROM departments";

		try
		{
			connection.Open();

			using var reader = command.ExecuteReader();

			if (reader.HasRows)
			{
				while (reader.Read())
				{
					departments.Add(new department
					{
						Id = reader.GetInt32(0),
						Title = reader.GetString(1)
					});
				}
				reader.Close();
				connection.Close();

				return departments;
			}
			reader.Close();
			connection.Close();

			return new List<Department>();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error: {ex.Message}");
		}

		return new List<Department>();
	}

	// GET BY ID: department
	public department GetById(int id)
	{
		var department = new department();

		using var connection = new SqlConnection(connectionString);
		using var command = new SqlCommand();

		command.Connection = connection;
		command.CommandText = "SELECT * FROM departments WHERE id = @id";
		command.Parameters.AddWithValue("@id", id);

		try
		{
			connection.Open();

			using var reader = command.ExecuteReader();

			if (reader.HasRows)
			{
				while (reader.Read())
				{
					department.Id = reader.GetInt32(0);
					department.Title = reader.GetString(1);
				}
			}
			else
			{
				Console.WriteLine("Data tidak ditemukan");
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error: {ex.Message}");
		}

		return department;
	}
	// INSERT: department
	public string Insert(string Title)
	{
		using var connection = new SqlConnection(connectionString);
		using var command = new SqlCommand();

		command.Connection = connection;
		command.CommandText = "INSERT INTO departments VALUES (@Title);";

		try
		{
			command.Parameters.Add(new SqlParameter("@Title", Title));

			connection.Open();
			using var transaction = connection.BeginTransaction();
			try
			{
				command.Transaction = transaction;

				var result = command.ExecuteNonQuery();

				transaction.Commit();
				connection.Close();

				return result.ToString();
			}
			catch (Exception ex)
			{
				transaction.Rollback();
				return $"Error Transaction: {ex.Message}";
			}
		}
		catch (Exception ex)
		{
			return $"Error: {ex.Message}";
		}
	}

	// UPDATE: department
	public string Update(int id, string Title)
	{
		using var connection = new SqlConnection(connectionString);
		using var command = new SqlCommand();

		command.Connection = connection;
		command.CommandText = "UPDATE departments SET Title = @Title WHERE id = @id";
		command.Parameters.AddWithValue("@id", id);
		command.Parameters.AddWithValue("@Title", Title);

		try
		{
			connection.Open();
			using var transaction = connection.BeginTransaction();
			try
			{
				command.Transaction = transaction;

				var result = command.ExecuteNonQuery();

				transaction.Commit();
				connection.Close();

				if (result >= 1)
				{
					return "Update Berhasil";
				}
				else
				{
					return "Data tidak ditemukan";
				}
			}
			catch (Exception ex)
			{
				transaction.Rollback();
				return $"Error Transaction: {ex.Message}";
			}
		}
		catch (Exception ex)
		{
			return $"Error: {ex.Message}";
		}
	}

	// DELETE: department
	public string Delete(int id)
	{
		using var connection = new SqlConnection(connectionString);
		using var command = new SqlCommand();

		command.Connection = connection;
		command.CommandText = "DELETE FROM departments WHERE id = @id";
		command.Parameters.AddWithValue("@id", id);

		try
		{
			connection.Open();
			using var transaction = connection.BeginTransaction();
			try
			{
				command.Transaction = transaction;

				var result = command.ExecuteNonQuery();

				transaction.Commit();
				connection.Close();

				if (result >= 1)
				{
					return "Berhasil Menghapus";
				}
				else
				{
					return "Data tidak ditemukan";
				}
			}
			catch (Exception ex)
			{
				transaction.Rollback();
				return $"Error Transaction: {ex.Message}";
			}
		}
		catch (Exception ex)
		{
			return $"Error: {ex.Message}";
		}
	}
}