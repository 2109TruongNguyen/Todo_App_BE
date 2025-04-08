namespace Application.Constants;

public static class MessageConstant
{
    public static class AuthMessage
    {
        // Default avatar url
        public const string DEFAULT_AVATAR_URL = "https://res.cloudinary.com/dkzn3xjwt/image/upload/v1742970651/7857e532-f639-4376-a874-229278182400.jpg";
        
        // Login
        public const string LOGIN_INVALID_REQUEST = "Invalid username or password";
        public const string LOGIN_SUCCESS = "Login successful";
        
        // Register
        public const string REGISTER_USERNAME_EXISTS = "Username already exists";
        public const string REGISTER_SUCCESS = "Register successful";
        public const string REGISTER_FAIL = "Register fail";
        
        // Refresh token
        public const string REFRESH_INVALID_TOKEN = "Refresh token is invalid";
        public const string REFRESH_USER_NOT_FOUND = "Cannot find user";
        public const string REFRESH_SUCCESS = "Refresh token successful";
    }
}