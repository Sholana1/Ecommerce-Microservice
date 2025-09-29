using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Google.Rpc;
using Grpc.Core;
using GoogleStatus = Google.Rpc.Status;
using GrpcStatus = Grpc.Core.Status;




namespace Discount.Extensions
{
    public static class GrpcErrorHelper
    {
        public static RpcException CreateValidationException(Dictionary<string, string> fieldError)
        {
            var fieldViolation = new List<BadRequest.Types.FieldViolation>();
            foreach(var error in fieldError)
            {
                fieldViolation.Add(new BadRequest.Types.FieldViolation()
                {
                    Field = error.Key,
                    Description = error.Value
                });
            }

            //Add Bad Request
            var badRequest = new BadRequest();
            badRequest.FieldViolations.AddRange(fieldViolation);

            var status = new GoogleStatus
            {
                Code = (int)StatusCode.InvalidArgument,
                Message = "Validation Failed",
                Details = { Any.Pack(badRequest) }
            };

            var trailers = new Metadata
            {
                {"grpc-status-details-bin", status.ToByteArray() }
            };

            return new RpcException(new GrpcStatus(StatusCode.InvalidArgument, "Validation errors"), trailers);

        }
    }
}
