using MediatR;
using SimulatedExchange.DataAccess.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimulatedExchange.DataAccess.ReportingTransaction.Orders
{
    public partial class OrderReporting :
        IReportingTransactionHandler<AddOrderTransaction>,
        IReportingTransactionHandler<UpdateOrderStatusTransaction>,
        IReportingTransactionHandler<UpdateOrderTransaction>,
        IReportingTransactionHandler<GetOrdersTransaction, GetOrdersTransactionResult>,
        IReportingTransactionHandler<GetOrderTransaction, GetOrderTransactionResult>
    {
        private static readonly ISet<PersistentObject> set;
        private readonly IOrderMapper mapper;

        static OrderReporting()
        {
            set = new HashSet<PersistentObject>();
        }

        public OrderReporting(IOrderMapper mapper)
        {
            this.mapper = mapper;
        }

        public Task<GetOrdersTransactionResult> Handle(GetOrdersTransaction request, CancellationToken cancellationToken)
        {
            IEnumerable<PersistentObject> datas = set.OrderByDescending(data => data.CreatedTimeUtc);

            var pageCount = 1;
            var pageIndex = 1;
            if (request.PagingOptions != null
                && request.PagingOptions.PageIndex > 0
                && request.PagingOptions.PageSize > 0)
            {
                pageIndex = request.PagingOptions.PageIndex;
                pageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(datas.Count()) / Convert.ToDouble(request.PagingOptions.PageSize)));
                datas = datas.Skip((request.PagingOptions.PageIndex - 1) * request.PagingOptions.PageSize)
                    .Take(request.PagingOptions.PageSize);
            }
            var result = new GetOrdersTransactionResult
            {
                PagingInfo =
                new PagingInfo { CurrentPageIndex = pageIndex, PageCount = pageCount }
            };

            foreach (var data in datas)
            {
                var item = mapper.MapToGetOrdersTransactionResultItem(data);
                result.Add(item);
            }

            return Task.FromResult(result);
        }

        public Task<Unit> Handle(AddOrderTransaction request, CancellationToken cancellationToken)
        {
            var po = mapper.MapFromAddOrderTransaction(request);
            set.Add(po);
            return Unit.Task;
        }

        public Task<Unit> Handle(UpdateOrderStatusTransaction request, CancellationToken cancellationToken)
        {
            var data = set.FirstOrDefault(c => c.Id == request.Id);
            if (data != null)
            {
                data.Status = request.Status;
                data.ModifyedTimeUtc = DateTime.UtcNow;
            }
            return Unit.Task;
        }

        public Task<Unit> Handle(UpdateOrderTransaction request, CancellationToken cancellationToken)
        {
            var data = set.FirstOrDefault(c => c.Id == request.Id);
            if (data != null)
            {
                data.Status = request.Status;
                data.Volume = request.Volume;
                data.ModifyedTimeUtc = DateTime.UtcNow;
            }
            return Unit.Task;
        }

        public Task<GetOrderTransactionResult> Handle(GetOrderTransaction request, CancellationToken cancellationToken)
        {
            var data = set.FirstOrDefault(c => c.Id == request.Id.ToString());
            var result = mapper.MapToGetOrderTransactionResult(data);
            return Task.FromResult(result);
        }
    }
}
