using System;
using System.Data.SqlClient;
using CompanyManagement.Database.Base;
using CompanyManagement.Utilities;

namespace CompanyManagement.Models;

public class TaskCheckOut
{
    private string checkInOutID = "";
    private string taskID = "";
    private DateTime updateDate = Utils.EMPTY_DATETIME;
    private string progress;

    public string CheckInOutID
    {
        get => checkInOutID;
        set => checkInOutID = value;
    }

    public string TaskID
    {
        get => taskID;
        set => taskID = value;
    }

    public DateTime UpdateDate
    {
        get => updateDate;
        set => updateDate = value;
    }

    public string Progress
    {
        get => progress;
        set => progress = value;
    }
    
    public TaskCheckOut() {}

    public TaskCheckOut(string checkInOutID, string taskID, DateTime updateDate, string progress)
    {
        this.checkInOutID = checkInOutID;
        this.taskID = taskID;
        this.updateDate = updateDate;
        this.progress = progress;
    }
    
    public TaskCheckOut(SqlDataReader reader)
    {
        try
        {
            checkInOutID = (string)reader[BaseDao.TASK_CHECK_OUT_TASK_ID];
            taskID = (string)reader[BaseDao.TASK_CHECK_OUT_TASK_ID];
            updateDate = reader.GetDateTime(reader.GetOrdinal(BaseDao.TASK_CHECK_OUT_UPDATE_DATE));
            progress = (string)reader[BaseDao.TASK_CHECK_OUT_PROGRESS];
        }
        catch (Exception e)
        {
            Log.Instance.Error(nameof(CheckInOut), "Error: " + e.Message);
        }
    }
}