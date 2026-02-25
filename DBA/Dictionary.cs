using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.IO;

/// <summary>
/// List Of All Fields Of Dictionary Table. [ Developed By Sajjad ]
/// </summary>
public enum DictionaryFields
{

	SNo, English, Urdu, AND__, OR__
	
}

/// <summary>
/// Class Extract From Table Dictionary. [ Developed By Sajjad ]
/// </summary>
/// <summary>
/// This Class Is Extract From Table Dictionary For DataBase Complete Data Manipulation. [ Developed By Sajjad If You Found Any Error In This Then Please Email Me At sajjad4any1@yahoo.com, hotmail.com, gmail.com It Open Source And Free For All.]
/// </summary>
partial class Dictionary
{

	#region Define Variables

	private int _SNo;	//INTEGER(10)
	private string _English;	//VARCHAR(255)
	private string _Urdu;	//VARCHAR(255)

	#endregion

	#region Constructors

	public Dictionary()
	{

		InitilizeVariables();

	}
	public Dictionary(int c_SNo)
	{

		#region Assigning Values

		_SNo = c_SNo;
		DataTable Ds;
		Ds = GetWhere(DictionaryFields.SNo, CND.EQUAL);
		if (Ds == null || Ds.Rows.Count < 1)
			throw new Exception(DictionaryFields.SNo.ToString() + " = " + _SNo + "Not Found!");
		InitilizeVariables();
		if (Ds.Rows[0]["SNo"] != DBNull.Value)
				_SNo = int.Parse(Ds.Rows[0]["SNo"].ToString());
		if (Ds.Rows[0]["English"] != DBNull.Value)
				_English = Ds.Rows[0]["English"].ToString().Trim();
		if (Ds.Rows[0]["Urdu"] != DBNull.Value)
				_Urdu = Ds.Rows[0]["Urdu"].ToString().Trim();

		#endregion
	
	}

	#endregion

	#region Properties

	public int SNo
	{
		get{ return _SNo; }
		set{ _SNo = value; }
	 }
	public string English
	{
		get{ return _English.Trim(); }
		set{ _English = value.Trim(); }
	}
	public string Urdu
	{
		get{ return _Urdu.Trim(); }
		set{ _Urdu = value.Trim(); }
	}


	#endregion

	#region Methods

	#region Get Data Methods


	/// <summary>
	/// For Get All From Dictionary Table's Selected Field. [ Developed By Sajjad ]
	/// </summary>
	public DataTable GetAll()
	{
		DataSet Ds;
		string SQL;
		try
		{
			SQL = "select * from [Dictionary]";
			Ds = DBA.ExecuteQuery( SQL );
			if( Ds.Tables.Count == 0 || Ds.Tables[0].Rows.Count == 0 )
				return null;
		}
		catch(Exception e)
		{
			Ds = null;
			throw new Exception("Can't Get Data From Dictionary Table.\nSystem Exception : " + e.Message);
		}
		return Ds.Tables[0];
	}

	/// <summary>
	/// For Get Data From Dictionary Table's Selected Field Base On Condition That You Give. [ Developed By Sajjad ]
	/// </summary>
	public DataTable GetWhere(DictionaryFields Target, CND Con)
	{
		DataSet Ds = new DataSet();
		string SQL = "";
		try
		{
			SQL = "Select * From [Dictionary] Where ";
			if(DictionaryFields.SNo == Target )
			{
				SQL += " [SNo] ";
				if(CND.EQUAL == Con )
					SQL += " = 0" + _SNo;
				else if(CND.GREATER == Con )
					SQL += " > 0" + _SNo;
				else if(CND.LESS == Con )
					SQL += " < 0" + _SNo;
				else if(CND.GREATER_OR_EQUAL == Con )
					SQL += " >= 0" + _SNo;
				else if(CND.LESS_OR_EQUAL == Con )
					SQL += " <= 0" + _SNo;
				else
					throw new Exception(Con.ToString() + " Condition Not Compatible With Field [SNo]");
			}
			else if(DictionaryFields.English == Target )
			{
				SQL += " [English] ";
				if(CND.EQUAL == Con )
					SQL += " = ' " + _English.Trim() + "'";
				else if(CND.LIKE == Con )
					SQL += "  LIKE '%" + _English.Trim() + "%'";
				else
					throw new Exception(Con.ToString() + " Condition Not Compatible With Field [English]");
			}
			else if(DictionaryFields.Urdu == Target )
			{
				SQL += " [Urdu] ";
				if(CND.EQUAL == Con )
					SQL += " = ' " + _Urdu.Trim() + "'";
				else if(CND.LIKE == Con )
					SQL += "  LIKE '%" + _Urdu.Trim() + "%'";
				else
					throw new Exception(Con.ToString() + " Condition Not Compatible With Field [Urdu]");
			}
			Ds = DBA.ExecuteQuery( SQL );
		}
		catch(Exception e)
		{
			Ds = null;
			throw new Exception("Can't Get Data From Dictionary Table.\nSystem Exception : " + e.Message);
		}
		return Ds.Tables[0];
	}

	/// <summary>
	/// Get Dictionary Class Objects Array (For Report Use Only). [ Develop By Sajjad ]
	/// </summary>
	/// <param name="dataTable">Data Table Must Contain Dictionary's Table Columns Only.</param>
	/// <returns></returns>
	public static Dictionary[] GetObjects(DataTable dataTable)
	{
		Dictionary[] obj = new Dictionary[dataTable.Rows.Count];
		try
		{
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
			
				obj[i] = new Dictionary();
				obj[i].SNo = int.Parse(dataTable.Rows[i][DictionaryFields.SNo.ToString()].ToString());
				obj[i].English = dataTable.Rows[i][DictionaryFields.English.ToString()].ToString();
				obj[i].Urdu = dataTable.Rows[i][DictionaryFields.Urdu.ToString()].ToString();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Error Occure During Creating Dictionary Objects Array!\nSystem Exception : " + ex.Message);
		}
		return obj;
	}

	/// <summary>
	/// For Get Data From Multiple Dictionary Table's Fields. [ Developed By Sajjad ]
	/// </summary>
	public DataTable GetWhereMulti(params DictionaryFields[] Target)
	{
		DataSet Ds = new DataSet();
		string SQL = "";
		try
		{
			SQL = "Select * From [Dictionary] Where ";
			int count=Target.GetLength(0);
			for (int i = 0; i < count; i++)
			{
				if(DictionaryFields.SNo == Target[i] )
				SQL += "[SNo] =0" + _SNo;
				 else if(DictionaryFields.English == Target[i] )
				SQL += "[English] =' " + _English.Trim() + "'";
				 else if(DictionaryFields.Urdu == Target[i] )
				SQL += "[Urdu] =' " + _Urdu.Trim() + "'";
				else if(DictionaryFields.AND__ == Target[i] )
					SQL += " AND ";
				else if(DictionaryFields.OR__ == Target[i] )
					SQL += " OR ";
				}
			Ds = DBA.ExecuteQuery( SQL );
		}
		catch(Exception e)
		{
			Ds = null;
			throw new Exception("Can't Get Data From Dictionary Table.\nSystem Exception : " + e.Message);
		}
		return Ds.Tables[0];
	}

	/// <summary>
	/// For Get Data From Multiple Dictionary Table's Fields Only For String Searching. [ Developed By Sajjad ]
	/// </summary>
	public DataTable GetWhereLike(params DictionaryFields[] Target)
	{
		DataSet Ds = new DataSet();
		string SQL = "";
		try
		{
			SQL = "Select * From [Dictionary] Where ";
			int count=Target.GetLength(0);
			for (int i = 0; i < count; i++)
			{
				if(DictionaryFields.English == Target[i] )
					SQL += "[English] Like ' %" + _English.Trim() + "%'";
				if(DictionaryFields.Urdu == Target[i] )
					SQL += "[Urdu] Like ' %" + _Urdu.Trim() + "%'";
				if (count - 1 > i)
					SQL += " AND ";
			}
			Ds = DBA.ExecuteQuery( SQL );
		}
		catch(Exception e)
		{
			Ds = null;
			throw new Exception("Can't Get Data From Dictionary Table.\nSystem Exception : " + e.Message);
		}
		return Ds.Tables[0];
	}

	/// <summary>
	/// For Get Max Value From Dictionary Table's Selected Field. [ Developed By Sajjad ]
	/// </summary>
	public int GetMax(DictionaryFields Target)
	{
		int EffectedRows = -1;
		string SQL = "";
		try
		{
			if(DictionaryFields.SNo == Target )
				SQL = "SELECT Max([SNo]) AS [Max Value] FROM [Dictionary]";
			else if(DictionaryFields.English == Target )
				SQL = "SELECT Max([English]) AS [Max Value] FROM [Dictionary]";
			else if(DictionaryFields.Urdu == Target )
				SQL = "SELECT Max([Urdu]) AS [Max Value] FROM [Dictionary]";
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
	/// For Get Missing Or Next Value From Dictionary Table. Field Must Be Numeric And Non-Auto. [ Developed By Sajjad ]
	/// </summary>
	public int GetNext(DictionaryFields Target)
	{
		int EffectedRows = -1;
		string SQL = "";
		try
		{
			if(DictionaryFields.SNo == Target )
				SQL = "SELECT [SNo] AS [Next Value] FROM [Dictionary]  ORDER BY [SNo]";
			else if(DictionaryFields.English == Target )
				SQL = "SELECT [English] AS [Next Value] FROM [Dictionary]  ORDER BY [English]";
			else if(DictionaryFields.Urdu == Target )
				SQL = "SELECT [Urdu] AS [Next Value] FROM [Dictionary]  ORDER BY [Urdu]";
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
	/// Set All Data Fields Of Dictionary Table Against First Field ( Primary Key ). [ Developed By Sajjad ]
	/// </summary>
	public int SetAll()
	{
		int EffectedRows;
		string SQL;
		try
		{
			SQL = "Update [Dictionary] Set ";
			SQL += "[English] =' " + _English.Trim() + "'";
			SQL += ",[Urdu] =' " + _Urdu.Trim() + "'";
			SQL += " Where [SNo] = " + _SNo;
			EffectedRows = DBA.ExecuteNonQuery( SQL );
		}
		catch(Exception e)
		{
			EffectedRows = -1;
			throw new Exception("Can't Update The Dictionary Table.\nSystem Exception : " + e.Message);
		}
		return EffectedRows;
	}

	/// <summary>
	///"UPDATE [Dictionary] SET [Target] = 3.9 WHERE [Source] = 1". [ Developed By Sajjad ]
	/// </summary>
	public int SetWhere(DictionaryFields Target, DictionaryFields Source, CND Con)
	{
		int EffectedRows = -1;
		string SQL = "";
		try
		{
			SQL = "Update [Dictionary] Set ";
			if(DictionaryFields.SNo == Target )
				SQL += "[SNo] =0" + _SNo;
			 else if(DictionaryFields.English == Target )
				SQL += "[English] =' " + _English.Trim() + "'";
			 else if(DictionaryFields.Urdu == Target )
				SQL += "[Urdu] =' " + _Urdu.Trim() + "'";
			SQL += " Where ";
			if(DictionaryFields.SNo == Source )
			{
				SQL += " [SNo] ";
				if(CND.EQUAL == Con )
					SQL += " = 0" + _SNo;
				else if(CND.GREATER == Con )
					SQL += " > 0" + _SNo;
				else if(CND.LESS == Con )
					SQL += " < 0" + _SNo;
				else if(CND.GREATER_OR_EQUAL == Con )
					SQL += " >= 0" + _SNo;
				else if(CND.LESS_OR_EQUAL == Con )
					SQL += " <= 0" + _SNo;
				else
					throw new Exception(Con.ToString() + " Condition Not Compatible With Field [SNo]");
			}
			else if(DictionaryFields.English == Source )
			{
				SQL += " [English] ";
				if(CND.EQUAL == Con )
					SQL += " = ' " + _English.Trim() + "'";
				else if(CND.LIKE == Con )
					SQL += "  LIKE '%" + _English.Trim() + "%'";
				else
					throw new Exception(Con.ToString() + " Condition Not Compatible With Field [English]");
			}
			else if(DictionaryFields.Urdu == Source )
			{
				SQL += " [Urdu] ";
				if(CND.EQUAL == Con )
					SQL += " = ' " + _Urdu.Trim() + "'";
				else if(CND.LIKE == Con )
					SQL += "  LIKE '%" + _Urdu.Trim() + "%'";
				else
					throw new Exception(Con.ToString() + " Condition Not Compatible With Field [Urdu]");
			}
			EffectedRows = DBA.ExecuteNonQuery( SQL );
		}
		catch(Exception e)
		{
			EffectedRows = -1;
			throw new Exception("Can't Update The Dictionary Table.\nSystem Exception : " + e.Message);
		}
		return EffectedRows;
	}

	#endregion

	#region Other Methods

	/// <summary>
	/// For Add New Record In Dictionary Table. [ Developed By Sajjad ]
	/// </summary>
	public int NewDictionary()
	{
		int EffectedRows;
		string SQL;
		try
		{
			if (_SNo == -1)
				_SNo = GetMax(DictionaryFields.SNo) + 1;
			SQL = "Insert into [Dictionary](";
			SQL += " [SNo]";
			SQL += ",[English]";
			SQL += ",[Urdu]";
			SQL += ") values(";
			SQL += "0"+ _SNo;
			SQL += ",' "+ _English.Trim() + "'";
			SQL += ",' "+ _Urdu.Trim() + "'";
			SQL += ")";
			EffectedRows = DBA.ExecuteNonQuery( SQL );
		}
		catch(Exception e)
		{
			EffectedRows = -1;
			throw new Exception("Can't Insert New Record In Dictionary Table.\nSystem Exception : " + e.Message);
		}
		return EffectedRows;
	}

	/// <summary>
	/// For ReSet All Field In Dictionary Class Object. [ Developed By Sajjad ]
	/// </summary>
	public void InitilizeVariables()
	{
		_SNo = 0;
		_English = "";
		_Urdu = "";

	}

	/// <summary>
	/// For Delete Record From Dictionary Table It Depend Upon Data In The Selected Field. [ Developed By Sajjad ]
	/// </summary>
	public int DeleteWhere(DictionaryFields Target, CND Con)
	{
		int EffectedRows = -1;
		string SQL;
		try
		{
			SQL = "Delete From [Dictionary] Where ";
			if(DictionaryFields.SNo == Target )
			{
				SQL += " [SNo] ";
				if(CND.EQUAL == Con )
					SQL += " = 0" + _SNo;
				else if(CND.GREATER == Con )
					SQL += " > 0" + _SNo;
				else if(CND.LESS == Con )
					SQL += " < 0" + _SNo;
				else if(CND.GREATER_OR_EQUAL == Con )
					SQL += " >= 0" + _SNo;
				else if(CND.LESS_OR_EQUAL == Con )
					SQL += " <= 0" + _SNo;
				else
					throw new Exception(Con.ToString() + " Condition Not Compatible With Field [SNo]");
			}
			else if(DictionaryFields.English == Target )
			{
				SQL += " [English] ";
				if(CND.EQUAL == Con )
					SQL += " = ' " + _English.Trim() + "'";
				else if(CND.LIKE == Con )
					SQL += "  LIKE '%" + _English.Trim() + "%'";
				else
					throw new Exception(Con.ToString() + " Condition Not Compatible With Field [English]");
			}
			else if(DictionaryFields.Urdu == Target )
			{
				SQL += " [Urdu] ";
				if(CND.EQUAL == Con )
					SQL += " = ' " + _Urdu.Trim() + "'";
				else if(CND.LIKE == Con )
					SQL += "  LIKE '%" + _Urdu.Trim() + "%'";
				else
					throw new Exception(Con.ToString() + " Condition Not Compatible With Field [Urdu]");
			}
			EffectedRows = DBA.ExecuteNonQuery( SQL );
		}
		catch(Exception e)
		{
			EffectedRows = -1;
			throw new Exception("Can't Delete Data From Dictionary Table.\nSystem Exception : " + e.Message);
		}
		return EffectedRows;
	}

	/// <summary>
	/// For Delete Record From Dictionary Table It Depend Upon Data In The Selected Fields. [ Developed By Sajjad ]
	/// </summary>
	public int DeleteWhereMulti(params DictionaryFields[] Target)
	{
		int EffectedRows = -1;
		string SQL = "";
		try
		{
			SQL = "Delete From [Dictionary] Where ";
			int count=Target.GetLength(0);
			for (int i = 0; i < count; i++)
			{
				if(DictionaryFields.SNo == Target[i] )
				SQL += "[SNo] =0" + _SNo;
					else if(DictionaryFields.English == Target[i] )
				SQL += "[English] =' " + _English.Trim() + "'";
					else if(DictionaryFields.Urdu == Target[i] )
				SQL += "[Urdu] =' " + _Urdu.Trim() + "'";
				else if(DictionaryFields.AND__ == Target[i] )
					SQL += " AND ";
				else if(DictionaryFields.OR__ == Target[i] )
					SQL += " OR ";
			}
			EffectedRows = DBA.ExecuteNonQuery( SQL );
		}
		catch(Exception e)
		{
			EffectedRows = -1;
			throw new Exception("Can't Delete Data From Dictionary Table.\nSystem Exception : " + e.Message);
		}
		return EffectedRows;
	}

	#endregion

	#endregion

}