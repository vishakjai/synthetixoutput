from fastapi import APIRouter, HTTPException, Depends
from pydantic import BaseModel
from typing import List

register_device_token = APIRouter()
delete_notification_token = APIRouter()
get_participants = APIRouter()
upload_profile_picture = APIRouter()
confirm_profile_picture = APIRouter()
delete_profile_picture = APIRouter()

class RegisterDeviceTokenRequest(BaseModel):
    username: str
    email: str
    password: str
    recaptcha_token: str

class RegisterDeviceTokenResponse(BaseModel):
    user_id: str
    username: str
    created_at: str

@register_device_token.post("/api/notification/register")
async def register_device_token_handler(request: RegisterDeviceTokenRequest):
    return RegisterDeviceTokenResponse(user_id="123", username=request.username, created_at="2023-10-01T12:00:00Z")

@delete_notification_token.delete("/api/notification/{token}")
async def delete_notification_token_handler(token: str):
    return {"status": "deleted"}

@get_participants.get("/api/participants")
async def get_participants_handler():
    return {"items": [], "total": 0, "page": 1, "page_size": 10}

@upload_profile_picture.post("/api/participants/profile-picture-upload")
async def upload_profile_picture_handler():
    return {"status": "uploaded"}

@confirm_profile_picture.post("/api/participants/confirm-profile-picture")
async def confirm_profile_picture_handler():
    return {"status": "confirmed"}

@delete_profile_picture.delete("/api/participants/profile-picture")
async def delete_profile_picture_handler():
    return {"status": "deleted"}