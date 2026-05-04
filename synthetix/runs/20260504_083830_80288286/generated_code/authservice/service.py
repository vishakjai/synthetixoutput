from models import SignupRequest, SignupResponse, LoginRequest, JwtAuthResponse, RefreshTokenRequest, LogoutResponse, PasswordResetRequest, PasswordResetResponse
from datetime import datetime, timedelta
import uuid

class AuthService:
    def register_user(self, request: SignupRequest) -> SignupResponse:
        user_id = str(uuid.uuid4())
        created_at = datetime.now()
        return SignupResponse(user_id=user_id, username=request.username, created_at=created_at)

    def login_user(self, request: LoginRequest) -> JwtAuthResponse:
        token = "dummy-token"  # Replace with real token generation logic
        expires_at = datetime.now() + timedelta(hours=1)
        return JwtAuthResponse(token=token, token_type="Bearer", username=request.username, authorities=[], expires_at=expires_at)

    def refresh_token(self, request: RefreshTokenRequest) -> JwtAuthResponse:
        token = "dummy-token"  # Replace with real token generation logic
        expires_at = datetime.now() + timedelta(hours=1)
        return JwtAuthResponse(token=token, token_type="Bearer", username="", authorities=[], expires_at=expires_at)

    def logout_user(self) -> LogoutResponse:
        return LogoutResponse(logged_out=True)

    def forgot_password(self, request: PasswordResetRequest) -> PasswordResetResponse:
        return PasswordResetResponse(reset_initiated=True)