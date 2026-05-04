from pydantic import BaseModel
from typing import List
from datetime import datetime
import uuid

class SignupRequest(BaseModel):
    username: str
    email: str
    password: str
    recaptcha_token: str

class SignupResponse(BaseModel):
    user_id: uuid.UUID
    username: str
    created_at: datetime

class LoginRequest(BaseModel):
    username: str
    password: str
    recaptcha_token: str

class JwtAuthResponse(BaseModel):
    token: str
    token_type: str
    username: str
    authorities: List[str]
    expires_at: datetime

class RefreshTokenRequest(BaseModel):
    refresh_token: str

class LogoutResponse(BaseModel):
    logged_out: bool

class PasswordResetRequest(BaseModel):
    email: str

class PasswordResetResponse(BaseModel):
    reset_initiated: bool