package main

type SignupRequest struct {
	Username       string `json:"username"`
	Email          string `json:"email"`
	Password       string `json:"password"`
	RecaptchaToken string `json:"recaptcha_token"`
}

type SignupResponse struct {
	UserID    string `json:"user_id"`
	Username  string `json:"username"`
	CreatedAt string `json:"created_at"`
}

type LoginRequest struct {
	Username       string `json:"username"`
	Password       string `json:"password"`
	RecaptchaToken string `json:"recaptcha_token"`
}

type JwtAuthResponse struct {
	Token       string   `json:"token"`
	TokenType   string   `json:"token_type"`
	Username    string   `json:"username"`
	Authorities []string `json:"authorities"`
	ExpiresAt   string   `json:"expires_at"`
}

type RefreshTokenRequest struct {
	RefreshToken string `json:"refresh_token"`
}

type LogoutResponse struct {
	LoggedOut bool `json:"logged_out"`
}

type PasswordResetRequest struct {
	Email string `json:"email"`
}

type PasswordResetResponse struct {
	ResetInitiated bool `json:"reset_initiated"`
}
