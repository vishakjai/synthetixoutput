from fastapi import APIRouter, HTTPException

register_device_token = APIRouter()

delete_notification_token = APIRouter()

get_participants = APIRouter()

upload_profile_picture = APIRouter()

confirm_profile_picture = APIRouter()

delete_profile_picture = APIRouter()

@register_device_token.post("/api/notification/register")
async def register_device_token_handler(username: str, email: str, password: str, recaptcha_token: str):
    # Simulate registration logic
    return {"user_id": "123", "username": username, "created_at": "2023-10-01T00:00:00Z"}

@delete_notification_token.delete("/api/notification/{token}")
async def delete_notification_token_handler(token: str):
    # Simulate deletion logic
    return {"status": "deleted"}

@get_participants.get("/api/participants")
async def get_participants_handler():
    # Simulate fetching participants
    return {"items": [], "total": 0, "page": 1, "page_size": 10}

@upload_profile_picture.post("/api/participants/profile-picture-upload")
async def upload_profile_picture_handler():
    # Simulate upload logic
    return {"status": "uploaded"}

@confirm_profile_picture.post("/api/participants/confirm-profile-picture")
async def confirm_profile_picture_handler():
    # Simulate confirmation logic
    return {"status": "confirmed"}

@delete_profile_picture.delete("/api/participants/profile-picture")
async def delete_profile_picture_handler():
    # Simulate deletion logic
    return {"status": "deleted"}