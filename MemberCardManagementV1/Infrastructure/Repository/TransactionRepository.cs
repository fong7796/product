using AspnetRun.Infrastructure.Repository;
using MemberCardManagementV1.Infrastructure.Repository.Interface;
using MemberCardManagementV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MemberCardManagementV1.Infrastructure.Repository
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
    }
}