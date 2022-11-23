using Agreement.Domain.Base;
using Agreement.Domain.Dto;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Agreement.Domain
{
    public partial class ApplicationDbContext
    {
        #region Store Procedure execute

        
        public List<AgreementDto> SP_GetAgreement(int start, int end, string orderby, string search)
            //(string jsonData, out bool? isSuccess, out string message)
        {
            
            SP_Execute jsonResult = new SP_Execute();
            var sqlQuery = $"EXEC SP_GET_AGREEMENTS @StartIndex, @EndIndex, @OrderBy, @Search";
            SqlParameter[] parameters =
            {
                new SqlParameter("@StartIndex", start),
                new SqlParameter("@EndIndex", end),
                new SqlParameter("@OrderBy", orderby),
                new SqlParameter("@Search", search),
                //new SqlParameter{ ParameterName = "@Message", DbType = DbType.AnsiString, Size = 500, Direction = ParameterDirection.Output, Value = message }
            };

            return this.AgreementList.FromSqlRaw(sqlQuery, parameters).AsNoTracking().AsEnumerable().ToList();

            //jsonResult = this.JsonResult.FromSqlRaw(sqlQuery, parameters).AsNoTracking().AsEnumerable().FirstOrDefault();
            //isSuccess = Convert.ToBoolean(parameters[1].Value);
            //message = Convert.ToString(parameters[2].Value);
            //return jsonResult == null ? null : jsonResult.JsonResult;
        }

        #endregion
    }
}
