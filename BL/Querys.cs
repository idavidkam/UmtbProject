using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
	static class Querys
	{
		/// <summary>
		/// IF UMTB_OperatorList table not exists in the database - this will create a new one with defult valus
		/// </summary>
		public static string CRAETE_OPERATOR_LIST_TABLE = @"IF (NOT EXISTS (SELECT * 
													                 FROM INFORMATION_SCHEMA.TABLES 
													                 WHERE TABLE_NAME = 'UMTB_OperatorList'))
													BEGIN
													    create table UMTB_OperatorList
														(
															Operator nvarchar(20),
															Logic nvarchar(MAX)
														);
													
														INSERT INTO UMTB_OperatorList VALUES
															('+', 'Addition: Performs addition between two fields, e.g., A + B.'),
															('-', 'Subtraction: Subtracts one element of the field from another, e.g., A - B.'),
															('*', 'Multiplication: Multiplies two elements within the same field, e.g., A * B.'),
															('/', 'Division: Divides one element of the field by another (if non-zero), e.g., A / B.')
													END";

		public static string GET_OPERATORS = "SELECT Operator FROM UMTB_OperatorList";

		/// <summary>
		/// create the TRANSACTION LOG table that holds the history
		/// </summary>
		public static string CREATE_TRANSACTION_LOG_TABLE = @"IF (NOT EXISTS (SELECT * 
															                  FROM INFORMATION_SCHEMA.TABLES 
															                  WHERE TABLE_NAME = 'UMTB_TransactionsLog'))
															 BEGIN
															     CREATE TABLE UMTB_TransactionsLog
															 	(
															 		ID int NOT NULL IDENTITY(1, 1),
															 		CreateDate datetime,
															 		Field1 nvarchar(MAX),
															 		Operator nvarchar(20),
															 		Field2 nvarchar(MAX),
															 		Result nvarchar(MAX)
															 	);
															 END";

		public static string ADD_NEW_RECORD_TO_TRANSACTION_LOG = @"INSERT INTO UMTB_TransactionsLog 
																	SELECT GETDATE(),@Field1,@Operator,@Field2,@Result";

		public static string GET_COUNT_RECORDES_PER_OPERATOR_FROM_THIS_MONTH = @"
										SELECT COUNT(*) FROM UMTB_TransactionsLog
										WHERE CreateDate >= DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1)
										AND Operator = @Operator";

		public static string GET_LAST_3_RECORDS_PER_OPERATOR = @"
										SELECT TOP 3 CreateDate,Field1,Operator,Field2,Result
										FROM UMTB_TransactionsLog
										WHERE Operator = @Operator
										ORDER BY CreateDate DESC";
    }
}