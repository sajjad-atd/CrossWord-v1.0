using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.IO;

/// <summary>
/// List Of Conditions Supported By This KIT [ Developed By Sajjad ]
/// </summary>
public enum CND
{

	EQUAL, NOT_EQUAL, GREATER, LESS, GREATER_OR_EQUAL, LESS_OR_EQUAL, LIKE

}

/// <summary>
/// List Of All Tables In DataBase [ Developed By Sajjad ]
/// </summary>
public enum TABLES
{

	Dictionary, Student, WordLearned
	
}

/// <summary>
/// Only For MS-Access DataBase [ Developed By Sajjad ]
/// </summary>
public partial class DBA
{

	private static string ConnStr;
	/// <summary>
	/// Dynamic Path Add Application Runtime Path With Access DataBase File [ Developed By Sajjad ]
	/// </summary>
	public static bool DynamicPath = false;
	/// <summary>
	/// Set The Access Database File Name [ Developed By Sajjad ]
	/// </summary>
	public static string AccessFileName
	{
		set { if (!DynamicPath) ConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + value.Trim() + ".MDB"; else ConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\" + value.Trim() + ".MDB"; }
		get { if (!DynamicPath) return ConnStr.Replace("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=", " ").Replace(".MDB", " ").Trim(); else return ConnStr.Replace("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=", " ").Replace(".MDB", " ").Replace(Application.StartupPath, " ").Replace("\\", " ").Trim(); }
	}
	/// <summary>
	/// Not For Select Command [ Developed By Sajjad ]
	/// </summary>
	public static int ExecuteNonQuery(string SQL)
	{
		int EffectedRow = 0;
		try
		{
			OleDbConnection Conn = new OleDbConnection(ConnStr);
			Conn.Open();
			OleDbCommand cmd = new OleDbCommand();
			cmd.Connection = Conn;
			cmd.CommandText = SQL;
			EffectedRow = cmd.ExecuteNonQuery();
			Conn.Close();
		}
		catch (Exception e)
		{
			EffectedRow = -1;
			throw new Exception("Error In Execute Non Query!\nSystem Exception : " + e.Message);
		}
		return EffectedRow;
	}
	/// <summary>
	/// Only For Select Command [ Developed By Sajjad ]
	/// </summary>
	public static DataSet ExecuteQuery(string SQL)
	{
		DataSet Ds = new DataSet();
		try
		{
			OleDbConnection Conn = new OleDbConnection(ConnStr);
			Conn.Open();
			OleDbDataAdapter Da = new OleDbDataAdapter(SQL, Conn);
			Da.Fill(Ds);
			Conn.Close();
		}
		catch(Exception e)
		{
			Ds = null;
		throw new Exception("Error In Execute Query!\nSystem Exception : " + e.Message);
		}
		return Ds;
	}
	/// <summary>
	/// Get Backup String For Selected Table [ Developed By Sajjad ]
	/// </summary>
	public static string Backup(TABLES tab)
	{
		string BK = "delete from [" + tab.ToString() + "];\n";
		try
		{
			string SQL = "select * from [" + tab + "];";
			DataSet ds = new DataSet();
			ds = ExecuteQuery(SQL);
			bool coma = false;
			for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
			{
				BK += "Insert into [" + tab + "] ([";
				for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
				{
					if (coma)
						BK += "], [" + ds.Tables[0].Columns[j].ToString();
					else
						BK += ds.Tables[0].Columns[j].ToString();
					coma = true;
					}
					BK += "]) values (";
					coma = false;
					for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
					{
					if (coma)
					{
						if (ds.Tables[0].Rows[i][j].ToString() == "True")
							BK += ", 1";
						else if (ds.Tables[0].Rows[i][j].ToString() == "False")
							BK += ", 0";
						else if (ds.Tables[0].Rows[i][j].ToString() == "False")
							BK += ", 0";
						else if (DBA.IsNumber(ds.Tables[0].Rows[i][j].ToString()))
							BK += ", 0" + ds.Tables[0].Rows[i][j].ToString();
						else if (IsDecimal(ds.Tables[0].Rows[i][j].ToString()))
							BK += ", 0" + ds.Tables[0].Rows[i][j].ToString();
						else if (IsDateTime(ds.Tables[0].Rows[i][j].ToString()))
							BK += ", '" + ds.Tables[0].Rows[i][j].ToString() + "'";
						else
							BK += ", ' " + ds.Tables[0].Rows[i][j].ToString().Trim() + "'";
					}
					else
					{
						if (ds.Tables[0].Rows[i][j].ToString() == "True")
							BK += "1";
						else if (ds.Tables[0].Rows[i][j].ToString() == "False")
							BK += "0";
						else if (ds.Tables[0].Rows[i][j].ToString() == "False")
							BK += "0";
						else if (IsNumber(ds.Tables[0].Rows[i][j].ToString()))
							BK += "0" + ds.Tables[0].Rows[i][j].ToString();
						else if (IsDecimal(ds.Tables[0].Rows[i][j].ToString()))
							BK += "0" + ds.Tables[0].Rows[i][j].ToString();
						else if (IsDateTime(ds.Tables[0].Rows[i][j].ToString()))
							BK += "'" + ds.Tables[0].Rows[i][j].ToString() + "'";
						else
							BK += "' " + ds.Tables[0].Rows[i][j].ToString().Trim() + "'";
					}
					coma = true;
				}
				coma = false;
				BK += ");\n";
				}
			}
		catch (Exception ex)
		{
			BK = "";
			throw new Exception("Error Occure During Taking Backup Of Table " + tab.ToString() + "!\nSystem Exception : " + ex.Message);
		}
	return BK;
	}
	/// <summary>
	/// Read String From File If Variable = "" Then Read Whole File Else Read Given Variable. [ Developed By Sajjad ]
	/// </summary>
	public static string ReadFromFile(string FileName, string Variable)
	{
		string Data = "";
		try
		{
			FileStream fs = new FileStream(FileName, FileMode.Open);
			int x = 0;
			while (fs.Length != x)
			{
				Data += Convert.ToChar((byte)fs.ReadByte());
				x++;
			}
			fs.Close();
			if (Variable != "")
			{
				Data = Data.Replace("\r", "");
				int index = Data.IndexOf(Variable);
				int index2 = Data.IndexOf("\n", index);
				Data = Data.Substring(index + Variable.Length + 1, index2 - (index + Variable.Length));
			}
		}
		catch (Exception ex)
		{
			Data = "";
			MessageBox.Show("Error Occure During Reading Data From " + FileName + " !\nSystem Exception : " + ex.Message);
		}
		return Data;
	}
	/// <summary>
	/// Write String To File [ Developed By Sajjad ]
	/// </summary>
	public static bool WriteToFile(string FileName, FileMode OpenAs, string Data)
	{
		bool b = false;
		try
		{
			FileStream fs = new FileStream(FileName, OpenAs);
			char[] ch = new char[Data.Length];
			ch = Data.ToCharArray();
			for (int i = 0; i < Data.Length; i++)
				fs.WriteByte(Convert.ToByte(ch[i]));
			fs.Close();
			b = true;
		}
		catch (Exception ex)
		{
			b = false;
			MessageBox.Show("Error Occure During Write Data On " + FileName + " !\nSystem Exception : " + ex.Message);
		}
		return b;
	}
	/// <summary>
	/// Check The String Is This Number [ Developed By Sajjad ]
	/// </summary>
	public static bool IsNumber(string Number)
	{
		bool b = false;
		try
		{
			int x = int.Parse(Number);
			b = true;
		}
		catch (Exception)
		{
			b = false;
		}
		return b;
	}
	/// <summary>
	/// Check The String Is This Date Time [ Developed By Sajjad ]
	/// </summary>
	public static bool IsDateTime(string DT)
	{
		bool b = false;
		try
		{
			DateTime x = DateTime.Parse(DT);
			b = true;
		}
		catch (Exception)
		{
			b = false;
		}
		return b;
	}
	/// <summary>
	/// Check The String Is This Decimal Or Floating Point Number [ Developed By Sajjad ]
	/// </summary>
	public static bool IsDecimal(string Decimal)
	{
		bool b = false;
		try
		{
			float x = float.Parse(Decimal);
			b = true;
		}
		catch (Exception)
		{
			b = false;
		}
		return b;
	}
}