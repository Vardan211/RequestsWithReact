using Requests.Application.Enums;
using Requests.Application.Exceptions;
using Requests.Application.Requests.V1.GetRequests;
using Requests.Domain.Models;

namespace Requests.Application.Extensions
{
    internal static class RequestExtensions
    {
        /// <summary>
        /// Метод для проверки согласованности
        /// </summary>
        /// <param name="request"><see cref="Request"/></param>
        /// <returns>Согласовано/Не согласовано</returns>
        public static bool? CheckApprove(this Request request)
        {
            if (request.PrimaryApprovers.Where(pa => pa.Approved.HasValue == true).Any(pa => pa.Approved == false)
                || request.SecondaryApprovers.Where(pa => pa.Approved.HasValue == true).Any(pa => pa.Approved == false))
            {
                return false;
            }

            if (request.PrimaryApprovers.All(pa => pa.Approved.HasValue == true && pa.Approved == true)
                && request.SecondaryApprovers.All(pa => pa.Approved.HasValue == true && pa.Approved == true))
            {
                return true;
            }

            return null;
        }

        /// <summary>
        /// Метод для проверки, может ли согласующий видеть конкретную заявку
        /// </summary>
        /// <typeparam name="T"><see cref="IApprovable"/></typeparam>
        /// <param name="request">Заявка</param>
        /// <param name="groupsConsecutive">Группы Согласующих по очереди</param>
        /// <param name="approver">Согласующий</param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public static bool CheckApproverCanSeeRequest<T>(this Request request, string[] groupsConsecutive, T approver)
            where T : IApprovable
        {
            _ = groupsConsecutive[0] ?? throw new AppException("Группа пустая");

            if (approver.GroupName == groupsConsecutive[0])
            {
                return true;
            }
            else
            {
                var indexOfGroup = Array.IndexOf(groupsConsecutive, approver.GroupName);
                var type = approver.GetType();

                switch (type.Name)
                {
                    case "PrimaryApprove":
                    if (!request.PrimaryApprovers
                    .Where(x => x.GroupName == groupsConsecutive[indexOfGroup - 1])
                    .Any(x => x.Approved.GetValueOrDefault() != true))
                    {
                        return true;
                    }

                    break;
                    case "SecondaryApprove":
                    if (!request.SecondaryApprovers
                    .Where(x => x.GroupName == groupsConsecutive[indexOfGroup - 1])
                    .Any(x => x.Approved.GetValueOrDefault() != true))
                    {
                        return true;
                    }

                    break;
                }
            }

            return false;
        }
    }
}
