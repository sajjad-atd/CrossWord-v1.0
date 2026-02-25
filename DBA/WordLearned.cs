using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.IO;

/// <summary>
/// List Of All Fields Of WordLearned Table. [ Developed By Sajjad ]
/// </summary>
public enum WordLearnedFields
{

	ID, StudentID, WordID, AND__, OR__
	
}

/// <summary>
/// Class Extract From Table WordLearned. [ Developed By Sajjad ]
/// </summary>
partial class WordLearned
{

	#region Define Variables

	private int _ID;	//INTEGER(10)
	private int _StudentID;	//INTEGER(10)
	private int _WordID;	//INTEGER(10)

	#endregion

	#region Constructors

	public WordLearned()
	{

		InitilizeVariables();

	}
	public WordLearned(int c_ID)
	{

		#region Assigning Values

		_ID = c_ID;
		DataTable Ds;
		Ds = GetWhere(WordLearnedFields.ID, CND.EQUAL);
		if (Ds == null || Ds.Rows.Count < 1)
			throw new Exception(WordLearnedFields.ID.ToString() + " = " + _ID + "Not Found!");
		InitilizeVariables();
		if (Ds.Rows[0]["ID"] != DBNull.Value)
				_ID = int.Parse(Ds.Rows[0]["ID"].ToString());
		if (Ds.Rows[0]["StudentID"] != DBNull.Value)
				_StudentID = int.Parse(Ds.Rows[0]["StudentID"].ToString());
		if (Ds.Rows[0]["WordID"] != DBNull.Value)
				_WordID = int.Parse(Ds.Rows[0]["WordID"].ToString());

		#endregion
	
	}

	#endregion

	#region Properties

	public int ID
	{
		get{ return _ID; }
		set{ _ID = value; }
	 }
	public int StudentID
	{
		get{ return _StudentID; }
		set{ _StudentID = value; }
	 }
	public int WordID
	{
		get{ return _WordID; }
		set{ _WordID = value; }
	 }


	#endregion

	#region Methods

	#region Get Data Methods


	/// <summary>
	/// For Get All From WordLearned Table's Selected Field. [ Developed By Sajjad ]
	/// </summary>
	public DataTable GetAll()
	{
		DataSet Ds;
		string SQL;
		try
		{
			SQL = "select * from [WordLearned]";
			Ds = DBA.ExecuteQuery( SQL );
			if( Ds.Tables.Count == 0 || Ds.Tables[0].Rows.Count == 0 )
				return null;
		}
		catch(Exception e)
		{
			Ds = null;
			throw new Exception("Can't Get Data From WordLearned Table.\nSystem Exception : " + e.Message);
		}
		return Ds.Tables[0];
	}

	/// <summary>
	/// For Get Data From WordLearned Table's Selected Field Base On Condition That You Give. [ Developed By Sajjad ]
	/// </summary>
	public DataTable GetWhere(WordLearnedFields Target, CND Con)
	{
		DataSet Ds = new DataSet();
		string SQL = "";
		try
		{
			SQL = "Select * From [WordLearned] Where ";
			if(WordLearnedFields.ID == Target )
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
			else if(WordLearnedFields.StudentID == Target )
			{
				SQL += " [StudentID] ";
				if(CND.EQUAL == Con )
					SQL += " = 0" + _StudentID;
				else if(CND.GREATER == Con )
					SQL += " > 0" + _StudentID;
				else if(CND.LESS == Con )
					SQL += " < 0" + _StudentID;
				else if(CND.GREATER_OR_EQUAL == Con )
					SQL += " >= 0" + _StudentID;
				else if(CND.LESS_OR_EQUAL == Con )
					SQL += " <= 0" + _StudentID;
				else
					throw new Exception(Con.ToString() + " Condition Not Compatible With Field [StudentID]");
			}
			else if(WordLearnedFields.WordID == Target )
			{
				SQL += " [WordID] ";
				if(CND.EQUAL == Con )
					SQL += " = 0" + _WordID;
				else if(CND.GREATER == Con )
					SQL += " > 0" + _WordID;
				else if(CND.LESS == Con )
					SQL += " < 0" + _WordID;
				else if(CND.GREATER_OR_EQUAL == Con )
					SQL += " >= 0" + _WordID;
				else if(CND.LESS_OR_EQUAL == Con )
					SQL += " <= 0" + _WordID;
				else
					throw new Exception(Con.ToString() + " Condition Not Compatible With Field [WordID]");
			}
			Ds = DBA.ExecuteQuery( SQL );
		}
		catch(Exception e)
		{
			Ds = null;
			throw new Exception("Can't Get Data From WordLearned Table.\nSystem Exception : " + e.Message);
		}
		return Ds.Tables[0];
	}

	/// <summary>
	/// Get WordLearned Class Objects Array (For Report Use Only). [ Develop By Sajjad ]
	/// </summary>
	/// <param name="dataTable">Data Table Must Contain WordLearned's Table Columns Only.</param>
	/// <returns></returns>
	public static WordLearned[] GetObjects(DataTable dataTable)
	{
		WordLearned[] obj = new WordLearned[dataTable.Rows.Count];
		try
		{
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
			
				obj[i] = new WordLearned();
				obj[i].ID = int.Parse(dataTable.Rows[i][WordLearnedFields.ID.ToString()].ToString());
				obj[i].StudentID = int.Parse(dataTable.Rows[i][WordLearnedFields.StudentID.ToString()].ToString());
				obj[i].WordID = int.Parse(dataTable.Rows[i][WordLearnedFields.WordID.ToString()].ToString());
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Error Occure During Creating WordLearned Objects Array!\nSystem Exception : " + ex.Message);
		}
		return obj;
	}

	/// <summary>
	/// For Get Data From Multiple WordLearned Table's Fields. [ Developed By Sajjad ]
	/// </summary>
	public DataTable GetWhereMulti(params WordLearnedFields[] Target)
	{
		DataSet Ds = new DataSet();
		string SQL = "";
		try
		{
			SQL = "Select * From [WordLearned] Where ";
			int count=Target.GetLength(0);
			for (int i = 0; i < count; i++)
			{
				if(WordLearnedFields.ID == Target[i] )
				SQL += "[ID] =0" + _ID;
				 else if(WordLearnedFields.StudentID == Target[i] )
				SQL += "[StudentID] =0" + _StudentID;
				 else if(WordLearnedFields.WordID == Target[i] )
				SQL += "[WordID] =0" + _WordID;
				else if(WordLearnedFields.AND__ == Target[i] )
					SQL += " AND ";
				else if(WordLearnedFields.OR__ == Target[i] )
					SQL += " OR ";
				}
			Ds = DBA.ExecuteQuery( SQL );
		}
		catch(Exception e)
		{
			Ds = null;
			throw new Exception("Can't Get Data From WordLearned Table.\nSystem Exception : " + e.Message);
		}
		return Ds.Tables[0];
	}

	/// <summary>
	/// For Get Data From Multiple WordLearned Table's Fields Only For String Searching. [ Developed By Sajjad ]
	/// </summary>
	public DataTable GetWhereLike(params WordLearnedFields[] Target)
	{
		DataSet Ds = new DataSet();
		string SQL = "";
		try
		{
			SQL = "Select * From [WordLearned] Where ";
			int count=Target.GetLength(0);
			for (int i = 0; i < count; i++)
			{
				if (count - 1 > i)
					SQL += " AND ";
			}
			Ds = DBA.ExecuteQuery( SQL );
		}
		catch(Exception e)
		{
			Ds = null;
			throw new Exception("Can't Get Data From WordLearned Table.\nSystem Exception : " + e.Message);
		}
		return Ds.Tables[0];
	}

	/// <summary>
	/// For Get Max Value From WordLearned Table's Selected Field. [ Developed By Sajjad ]
	/// </summary>
	public int GetMax(WordLearnedFields Target)
	{
		int EffectedRows = -1;
		string SQL = "";
		try
		{
			if(WordLearnedFields.ID == Target )
				SQL = "SELECT Max([ID]) AS [Max Value] FROM [WordLearned]";
			else if(WordLearnedFields.StudentID == Target )
				SQL = "SELECT Max([StudentID]) AS [Max Value] FROM [WordLearned]";
			else if(WordLearnedFields.WordID == Target )
				SQL = "SELECT Max([WordID]) AS [Max Value] FROM [WordLearned]";
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
	/// For Get Missing Or Next Value From WordLearned Table. Field Must Be Numeric And Non-Auto. [ Developed By Sajjad ]
	/// </summary>
	public int GetNext(WordLearnedFields Target)
	{
		int EffectedRows = -1;
		string SQL = "";
		try
		{
			if(WordLearnedFields.ID == Target )
				SQL = "SELECT [ID] AS [Next Value] FROM [WordLearned]  ORDER BY [ID]";
			else if(WordLearnedFields.StudentID == Target )
				SQL = "SELECT [StudentID] AS [Next Value] FROM [WordLearned]  ORDER BY [StudentID]";
			else if(WordLearnedFields.WordID == Target )
				SQL = "SELECT [WordID] AS [Next Value] FROM [WordLearned]  ORDER BY [WordID]";
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
	/// Set All Data Fields Of WordLearned Table Against First Field ( Primary Key ). [ Developed By Sajjad ]
	/// </summary>
	public int SetAll()
	{
		int EffectedRows;
		string SQL;
		try
		{
			SQL = "Update [WordLearned] Set ";
			SQL += "[StudentID] =0" + _StudentID;
			SQL += ",[WordID] =0" + _WordID;
			SQL += " Where [ID] = " + _ID;
			EffectedRows = DBA.ExecuteNonQuery( SQL );
		}
		catch(Exception e)
		{
			EffectedRows = -1;
			throw new Exception("Can't Update The WordLearned Table.\nSystem Exception : " + e.Message);
		}
		return EffectedRows;
	}

	/// <summary>
	///"UPDATE [WordLearned] SET [Target] = 3.9 WHERE [Source] = 1". [ Developed By Sajjad ]
	/// </summary>
	public int SetWhere(WordLearnedFields Target, WordLearnedFields Source, CND Con)
	{
		int EffectedRows = -1;
		string SQL = "";
		try
		{
			SQL = "Update [WordLearned] Set ";
			if(WordLearnedFields.ID == Target )
				SQL += "[ID] =0" + _ID;
			 else if(WordLearnedFields.StudentID == Target )
				SQL += "[StudentID] =0" + _StudentID;
			 else if(WordLearnedFields.WordID == Target )
				SQL += "[WordID] =0" + _WordID;
			SQL += " Where ";
			if(WordLearnedFields.ID == Source )
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
			else if(WordLearnedFields.StudentID == Source )
			{
				SQL += " [StudentID] ";
				if(CND.EQUAL == Con )
					SQL += " = 0" + _StudentID;
				else if(CND.GREATER == Con )
					SQL += " > 0" + _StudentID;
				else if(CND.LESS == Con )
					SQL += " < 0" + _StudentID;
				else if(CND.GREATER_OR_EQUAL == Con )
					SQL += " >= 0" + _StudentID;
				else if(CND.LESS_OR_EQUAL == Con )
					SQL += " <= 0" + _StudentID;
				else
					throw new Exception(Con.ToString() + " Condition Not Compatible With Field [StudentID]");
			}
			else if(WordLearnedFields.WordID == Source )
			{
				SQL += " [WordID] ";
				if(CND.EQUAL == Con )
					SQL += " = 0" + _WordID;
				else if(CND.GREATER == Con )
					SQL += " > 0" + _WordID;
				else if(CND.LESS == Con )
					SQL += " < 0" + _WordID;
				else if(CND.GREATER_OR_EQUAL == Con )
					SQL += " >= 0" + _WordID;
				else if(CND.LESS_OR_EQUAL == Con )
					SQL += " <= 0" + _WordID;
				else
					throw new Exception(Con.ToString() + " Condition Not Compatible With Field [WordID]");
			}
			EffectedRows = DBA.ExecuteNonQuery( SQL );
		}
		catch(Exception e)
		{
			EffectedRows = -1;
			throw new Exception("Can't Update The WordLearned Table.\nSystem Exception : " + e.Message);
		}
		return EffectedRows;
	}

	#endregion

	#region Other Methods

	/// <summary>
	/// For Add New Record In WordLearned Table. [ Developed By Sajjad ]
	/// </summary>
	public int NewWordLearned()
	{
		int EffectedRows;
		string SQL;
		try
		{
			if (_ID == -1)
				_ID = GetMax(WordLearnedFields.ID) + 1;
			SQL = "Insert into [WordLearned](";
			SQL += " [ID]";
			SQL += ",[StudentID]";
			SQL += ",[WordID]";
			SQL += ") values(";
			SQL += "0"+ _ID;
			SQL += ",0"+ _StudentID;
			SQL += ",0"+ _WordID;
			SQL += ")";
			EffectedRows = DBA.ExecuteNonQuery( SQL );
		}
		catch(Exception e)
		{
			EffectedRows = -1;
			throw new Exception("Can't Insert New Record In WordLearned Table.\nSystem Exception : " + e.Message);
		}
		return EffectedRows;
	}

	/// <summary>
	/// For ReSet All Field In WordLearned Class Object. [ Developed By Sajjad ]
	/// </summary>
	public void InitilizeVariables()
	{
		_ID = 0;
		_StudentID = 0;
		_WordID = 0;

	}

	/// <summary>
	/// For Delete Record From WordLearned Table It Depend Upon Data In The Selected Field. [ Developed By Sajjad ]
	/// </summary>
	public int DeleteWhere(WordLearnedFields Target, CND Con)
	{
		int EffectedRows = -1;
		string SQL;
		try
		{
			SQL = "Delete From [WordLearned] Where ";
			if(WordLearnedFields.ID == Target )
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
			else if(WordLearnedFields.StudentID == Target )
			{
				SQL += " [StudentID] ";
				if(CND.EQUAL == Con )
					SQL += " = 0" + _StudentID;
				else if(CND.GREATER == Con )
					SQL += " > 0" + _StudentID;
				else if(CND.LESS == Con )
					SQL += " < 0" + _StudentID;
				else if(CND.GREATER_OR_EQUAL == Con )
					SQL += " >= 0" + _StudentID;
				else if(CND.LESS_OR_EQUAL == Con )
					SQL += " <= 0" + _StudentID;
				else
					throw new Exception(Con.ToString() + " Condition Not Compatible With Field [StudentID]");
			}
			else if(WordLearnedFields.WordID == Target )
			{
				SQL += " [WordID] ";
				if(CND.EQUAL == Con )
					SQL += " = 0" + _WordID;
				else if(CND.GREATER == Con )
					SQL += " > 0" + _WordID;
				else if(CND.LESS == Con )
					SQL += " < 0" + _WordID;
				else if(CND.GREATER_OR_EQUAL == Con )
					SQL += " >= 0" + _WordID;
				else if(CND.LESS_OR_EQUAL == Con )
					SQL += " <= 0" + _WordID;
				else
					throw new Exception(Con.ToString() + " Condition Not Compatible With Field [WordID]");
			}
			EffectedRows = DBA.ExecuteNonQuery( SQL );
		}
		catch(Exception e)
		{
			EffectedRows = -1;
			throw new Exception("Can't Delete Data From WordLearned Table.\nSystem Exception : " + e.Message);
		}
		return EffectedRows;
	}

	/// <summary>
	/// For Delete Record From WordLearned Table It Depend Upon Data In The Selected Fields. [ Developed By Sajjad ]
	/// </summary>
	public int DeleteWhereMulti(params WordLearnedFields[] Target)
	{
		int EffectedRows = -1;
		string SQL = "";
		try
		{
			SQL = "Delete From [WordLearned] Where ";
			int count=Target.GetLength(0);
			for (int i = 0; i < count; i++)
			{
				if(WordLearnedFields.ID == Target[i] )
				SQL += "[ID] =0" + _ID;
					else if(WordLearnedFields.StudentID == Target[i] )
				SQL += "[StudentID] =0" + _StudentID;
					else if(WordLearnedFields.WordID == Target[i] )
				SQL += "[WordID] =0" + _WordID;
				else if(WordLearnedFields.AND__ == Target[i] )
					SQL += " AND ";
				else if(WordLearnedFields.OR__ == Target[i] )
					SQL += " OR ";
			}
			EffectedRows = DBA.ExecuteNonQuery( SQL );
		}
		catch(Exception e)
		{
			EffectedRows = -1;
			throw new Exception("Can't Delete Data From WordLearned Table.\nSystem Exception : " + e.Message);
		}
		return EffectedRows;
	}

	#endregion

	#endregion

}