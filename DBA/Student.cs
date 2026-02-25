using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.IO;

/// <summary>
/// List Of All Fields Of Student Table. [ Developed By Sajjad ]
/// </summary>
public enum StudentFields
{

	ID, StudentName, AND__, OR__
	
}

/// <summary>
/// Class Extract From Table Student. [ Developed By Sajjad ]
/// </summary>
partial class Student
{

	#region Define Variables

	private int _ID;	//INTEGER(10)
	private string _StudentName;	//VARCHAR(50)

	#endregion

	#region Constructors

	public Student()
	{

		InitilizeVariables();

	}
	public Student(int c_ID)
	{

		#region Assigning Values

		_ID = c_ID;
		DataTable Ds;
		Ds = GetWhere(StudentFields.ID, CND.EQUAL);
		if (Ds == null || Ds.Rows.Count < 1)
			throw new Exception(StudentFields.ID.ToString() + " = " + _ID + "Not Found!");
		InitilizeVariables();
		if (Ds.Rows[0]["ID"] != DBNull.Value)
				_ID = int.Parse(Ds.Rows[0]["ID"].ToString());
		if (Ds.Rows[0]["StudentName"] != DBNull.Value)
				_StudentName = Ds.Rows[0]["StudentName"].ToString().Trim();

		#endregion
	
	}

	#endregion

	#region Properties

	public int ID
	{
		get{ return _ID; }
		set{ _ID = value; }
	 }
	public string StudentName
	{
		get{ return _StudentName.Trim(); }
		set{ _StudentName = value.Trim(); }
	}


	#endregion

	#region Methods

	#region Get Data Methods


	/// <summary>
	/// For Get All From Student Table's Selected Field. [ Developed By Sajjad ]
	/// </summary>
	public DataTable GetAll()
	{
		DataSet Ds;
		string SQL;
		try
		{
			SQL = "select * from [Student]";
			Ds = DBA.ExecuteQuery( SQL );
			if( Ds.Tables.Count == 0 || Ds.Tables[0].Rows.Count == 0 )
				return null;
		}
		catch(Exception e)
		{
			Ds = null;
			throw new Exception("Can't Get Data From Student Table.\nSystem Exception : " + e.Message);
		}
		return Ds.Tables[0];
	}

	/// <summary>
	/// For Get Data From Student Table's Selected Field Base On Condition That You Give. [ Developed By Sajjad ]
	/// </summary>
	public DataTable GetWhere(StudentFields Target, CND Con)
	{
		DataSet Ds = new DataSet();
		string SQL = "";
		try
		{
			SQL = "Select * From [Student] Where ";
			if(StudentFields.ID == Target )
			{
				SQL += " [ID] ";
				if(CND.EQUAL == Con )
					SQL += " = 0" + _ID;
				else if(CND.GREATER == Con )
					SQL += " > 0" + _ID;
				else if(CND.LESS == Con )
					SQL += " < 0" + _ID;
				else if(CND.GREATER_OR_EQUAL == Con )
					SQL += " >= 0" + _ID;
				else if(CND.LESS_OR_EQUAL == Con )
					SQL += " <= 0" + _ID;
				else
					throw new Exception(Con.ToString() + " Condition Not Compatible With Field [ID]");
			}
			else if(StudentFields.StudentName == Target )
			{
				SQL += " [StudentName] ";
				if(CND.EQUAL == Con )
					SQL += " = ' " + _StudentName.Trim() + "'";
				else if(CND.LIKE == Con )
					SQL += "  LIKE '%" + _StudentName.Trim() + "%'";
				else
					throw new Exception(Con.ToString() + " Condition Not Compatible With Field [StudentName]");
			}
			Ds = DBA.ExecuteQuery( SQL );
		}
		catch(Exception e)
		{
			Ds = null;
			throw new Exception("Can't Get Data From Student Table.\nSystem Exception : " + e.Message);
		}
		return Ds.Tables[0];
	}

	/// <summary>
	/// Get Student Class Objects Array (For Report Use Only). [ Develop By Sajjad ]
	/// </summary>
	/// <param name="dataTable">Data Table Must Contain Student's Table Columns Only.</param>
	/// <returns></returns>
	public static Student[] GetObjects(DataTable dataTable)
	{
		Student[] obj = new Student[dataTable.Rows.Count];
		try
		{
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
			
				obj[i] = new Student();
				obj[i].ID = int.Parse(dataTable.Rows[i][StudentFields.ID.ToString()].ToString());
				obj[i].StudentName = dataTable.Rows[i][StudentFields.StudentName.ToString()].ToString();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Error Occure During Creating Student Objects Array!\nSystem Exception : " + ex.Message);
		}
		return obj;
	}

	/// <summary>
	/// For Get Data From Multiple Student Table's Fields. [ Developed By Sajjad ]
	/// </summary>
	public DataTable GetWhereMulti(params StudentFields[] Target)
	{
		DataSet Ds = new DataSet();
		string SQL = "";
		try
		{
			SQL = "Select * From [Student] Where ";
			int count=Target.GetLength(0);
			for (int i = 0; i < count; i++)
			{
				if(StudentFields.ID == Target[i] )
				SQL += "[ID] =0" + _ID;
				 else if(StudentFields.StudentName == Target[i] )
				SQL += "[StudentName] =' " + _StudentName.Trim() + "'";
				else if(StudentFields.AND__ == Target[i] )
					SQL += " AND ";
				else if(StudentFields.OR__ == Target[i] )
					SQL += " OR ";
				}
			Ds = DBA.ExecuteQuery( SQL );
		}
		catch(Exception e)
		{
			Ds = null;
			throw new Exception("Can't Get Data From Student Table.\nSystem Exception : " + e.Message);
		}
		return Ds.Tables[0];
	}

	/// <summary>
	/// For Get Data From Multiple Student Table's Fields Only For String Searching. [ Developed By Sajjad ]
	/// </summary>
	public DataTable GetWhereLike(params StudentFields[] Target)
	{
		DataSet Ds = new DataSet();
		string SQL = "";
		try
		{
			SQL = "Select * From [Student] Where ";
			int count=Target.GetLength(0);
			for (int i = 0; i < count; i++)
			{
				if(StudentFields.StudentName == Target[i] )
					SQL += "[StudentName] Like ' %" + _StudentName.Trim() + "%'";
				if (count - 1 > i)
					SQL += " AND ";
			}
			Ds = DBA.ExecuteQuery( SQL );
		}
		catch(Exception e)
		{
			Ds = null;
			throw new Exception("Can't Get Data From Student Table.\nSystem Exception : " + e.Message);
		}
		return Ds.Tables[0];
	}

	/// <summary>
	/// For Get Max Value From Student Table's Selected Field. [ Developed By Sajjad ]
	/// </summary>
	public int GetMax(StudentFields Target)
	{
		int EffectedRows = -1;
		string SQL = "";
		try
		{
			if(StudentFields.ID == Target )
				SQL = "SELECT Max([ID]) AS [Max Value] FROM [Student]";
			else if(StudentFields.StudentName == Target )
				SQL = "SELECT Max([StudentName]) AS [Max Value] FROM [Student]";
			DataSet Ds = new DataSet();
			Ds = DBA.ExecuteQuery( SQL );
			if(Ds.Tables[0].Rows.Count > 0 && Ds.Tables[0].Rows[0][0].ToString() != "")
				EffectedRows = int.Parse(Ds.Tables[0].Rows[0][0].ToString());
			else
				EffectedRows = 0;
		}
		catch(Exception e)
		{
			EffectedRows = -1;
			throw new Exception("Can't Getting Max Value From " + Target.ToString() + " Field.\nSystem Exception : " + e.Message);
		}
		return EffectedRows;
	}

	/// <summary>
	/// For Get Missing Or Next Value From Student Table. Field Must Be Numeric And Non-Auto. [ Developed By Sajjad ]
	/// </summary>
	public int GetNext(StudentFields Target)
	{
		int EffectedRows = -1;
		string SQL = "";
		try
		{
			if(StudentFields.ID == Target )
				SQL = "SELECT [ID] AS [Next Value] FROM [Student]  ORDER BY [ID]";
			else if(StudentFields.StudentName == Target )
				SQL = "SELECT [StudentName] AS [Next Value] FROM [Student]  ORDER BY [StudentName]";
			DataSet Ds = new DataSet();
			Ds = DBA.ExecuteQuery( SQL );
			if (int.Parse(Ds.Tables[0].Rows[0][0].ToString()) != 0)
				return 0;
			else
				for (int i = 1; i < Ds.Tables[0].Rows.Count; i++)
					if (int.Parse(Ds.Tables[0].Rows[i][0].ToString()) != i)
						return i;
		}
		catch(Exception e)
		{
			EffectedRows = -1;
			throw new Exception("Can't Getting Next Value From " + Target.ToString() + " Field.\nSystem Exception : " + e.Message);
		}
		return EffectedRows;
	}

	#endregion

	#region Set Data Methods

	/// <summary>
	/// Set All Data Fields Of Student Table Against First Field ( Primary Key ). [ Developed By Sajjad ]
	/// </summary>
	public int SetAll()
	{
		int EffectedRows;
		string SQL;
		try
		{
			SQL = "Update [Student] Set ";
			SQL += "[StudentName] =' " + _StudentName.Trim() + "'";
			SQL += " Where [ID] = " + _ID;
			EffectedRows = DBA.ExecuteNonQuery( SQL );
		}
		catch(Exception e)
		{
			EffectedRows = -1;
			throw new Exception("Can't Update The Student Table.\nSystem Exception : " + e.Message);
		}
		return EffectedRows;
	}

	/// <summary>
	///"UPDATE [Student] SET [Target] = 3.9 WHERE [Source] = 1". [ Developed By Sajjad ]
	/// </summary>
	public int SetWhere(StudentFields Target, StudentFields Source, CND Con)
	{
		int EffectedRows = -1;
		string SQL = "";
		try
		{
			SQL = "Update [Student] Set ";
			if(StudentFields.ID == Target )
				SQL += "[ID] =0" + _ID;
			 else if(StudentFields.StudentName == Target )
				SQL += "[StudentName] =' " + _StudentName.Trim() + "'";
			SQL += " Where ";
			if(StudentFields.ID == Source )
			{
				SQL += " [ID] ";
				if(CND.EQUAL == Con )
					SQL += " = 0" + _ID;
				else if(CND.GREATER == Con )
					SQL += " > 0" + _ID;
				else if(CND.LESS == Con )
					SQL += " < 0" + _ID;
				else if(CND.GREATER_OR_EQUAL == Con )
					SQL += " >= 0" + _ID;
				else if(CND.LESS_OR_EQUAL == Con )
					SQL += " <= 0" + _ID;
				else
					throw new Exception(Con.ToString() + " Condition Not Compatible With Field [ID]");
			}
			else if(StudentFields.StudentName == Source )
			{
				SQL += " [StudentName] ";
				if(CND.EQUAL == Con )
					SQL += " = ' " + _StudentName.Trim() + "'";
				else if(CND.LIKE == Con )
					SQL += "  LIKE '%" + _StudentName.Trim() + "%'";
				else
					throw new Exception(Con.ToString() + " Condition Not Compatible With Field [StudentName]");
			}
			EffectedRows = DBA.ExecuteNonQuery( SQL );
		}
		catch(Exception e)
		{
			EffectedRows = -1;
			throw new Exception("Can't Update The Student Table.\nSystem Exception : " + e.Message);
		}
		return EffectedRows;
	}

	#endregion

	#region Other Methods

	/// <summary>
	/// For Add New Record In Student Table. [ Developed By Sajjad ]
	/// </summary>
	public int NewStudent()
	{
		int EffectedRows;
		string SQL;
		try
		{
			if (_ID == -1)
				_ID = GetMax(StudentFields.ID) + 1;
			SQL = "Insert into [Student](";
			SQL += " [ID]";
			SQL += ",[StudentName]";
			SQL += ") values(";
			SQL += "0"+ _ID;
			SQL += ",' "+ _StudentName.Trim() + "'";
			SQL += ")";
			EffectedRows = DBA.ExecuteNonQuery( SQL );
		}
		catch(Exception e)
		{
			EffectedRows = -1;
			throw new Exception("Can't Insert New Record In Student Table.\nSystem Exception : " + e.Message);
		}
		return EffectedRows;
	}

	/// <summary>
	/// For ReSet All Field In Student Class Object. [ Developed By Sajjad ]
	/// </summary>
	public void InitilizeVariables()
	{
		_ID = 0;
		_StudentName = "";

	}

	/// <summary>
	/// For Delete Record From Student Table It Depend Upon Data In The Selected Field. [ Developed By Sajjad ]
	/// </summary>
	public int DeleteWhere(StudentFields Target, CND Con)
	{
		int EffectedRows = -1;
		string SQL;
		try
		{
			SQL = "Delete From [Student] Where ";
			if(StudentFields.ID == Target )
			{
				SQL += " [ID] ";
				if(CND.EQUAL == Con )
					SQL += " = 0" + _ID;
				else if(CND.GREATER == Con )
					SQL += " > 0" + _ID;
				else if(CND.LESS == Con )
					SQL += " < 0" + _ID;
				else if(CND.GREATER_OR_EQUAL == Con )
					SQL += " >= 0" + _ID;
				else if(CND.LESS_OR_EQUAL == Con )
					SQL += " <= 0" + _ID;
				else
					throw new Exception(Con.ToString() + " Condition Not Compatible With Field [ID]");
			}
			else if(StudentFields.StudentName == Target )
			{
				SQL += " [StudentName] ";
				if(CND.EQUAL == Con )
					SQL += " = ' " + _StudentName.Trim() + "'";
				else if(CND.LIKE == Con )
					SQL += "  LIKE '%" + _StudentName.Trim() + "%'";
				else
					throw new Exception(Con.ToString() + " Condition Not Compatible With Field [StudentName]");
			}
			EffectedRows = DBA.ExecuteNonQuery( SQL );
		}
		catch(Exception e)
		{
			EffectedRows = -1;
			throw new Exception("Can't Delete Data From Student Table.\nSystem Exception : " + e.Message);
		}
		return EffectedRows;
	}

	/// <summary>
	/// For Delete Record From Student Table It Depend Upon Data In The Selected Fields. [ Developed By Sajjad ]
	/// </summary>
	public int DeleteWhereMulti(params StudentFields[] Target)
	{
		int EffectedRows = -1;
		string SQL = "";
		try
		{
			SQL = "Delete From [Student] Where ";
			int count=Target.GetLength(0);
			for (int i = 0; i < count; i++)
			{
				if(StudentFields.ID == Target[i] )
				SQL += "[ID] =0" + _ID;
					else if(StudentFields.StudentName == Target[i] )
				SQL += "[StudentName] =' " + _StudentName.Trim() + "'";
				else if(StudentFields.AND__ == Target[i] )
					SQL += " AND ";
				else if(StudentFields.OR__ == Target[i] )
					SQL += " OR ";
			}
			EffectedRows = DBA.ExecuteNonQuery( SQL );
		}
		catch(Exception e)
		{
			EffectedRows = -1;
			throw new Exception("Can't Delete Data From Student Table.\nSystem Exception : " + e.Message);
		}
		return EffectedRows;
	}

	#endregion

	#endregion

}