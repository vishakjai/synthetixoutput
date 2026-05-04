from fastapi import APIRouter, HTTPException, Depends
from models import SignupRequest, SignupResponse, LoginRequest, JwtAuthResponse, RefreshTokenRequest, LogoutResponse, PasswordResetRequest, PasswordResetResponse
from service import AuthService

router = APIRouter()

auth_service = AuthService()

@router.post("/api/auth/apiKey")
def create_api_key():
    return {"status": "success"}

@router.post("/api/auth/register", response_model=SignupResponse)
def register_user(request: SignupRequest):
    return auth_service.register_user(request)

@router.post("/api/auth/login", response_model=JwtAuthResponse)
def login_user(request: LoginRequest):
    return auth_service.login_user(request)

@router.post("/api/auth/refresh", response_model=JwtAuthResponse)
def refresh_token(request: RefreshTokenRequest):
    return auth_service.refresh_token(request)

@router.post("/api/auth/logout", response_model=LogoutResponse)
def logout_user():
    return auth_service.logout_user()

@router.post("/api/auth/resend-verification")
def resend_verification():
    return {"status": "success"}

@router.post("/api/auth/verify")
def verify_user():
    return {"status": "success"}

@router.post("/api/auth/forgot-password", response_model=PasswordResetResponse)
def forgot_password(request: PasswordResetRequest):
    return auth_service.forgot_password(request)