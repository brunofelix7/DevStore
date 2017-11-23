namespace DevStore.Api.Response {

    public class ServerResponse {

        public int Status { get; set; }
        public string Message { get; set; }
        public string Errors { get; set; }

        public ServerResponse() {

        }

        public ServerResponse(int status, string message, string errors) {
            this.Status = status;
            this.Message = message;
            this.Errors = errors;
        }
    }
}