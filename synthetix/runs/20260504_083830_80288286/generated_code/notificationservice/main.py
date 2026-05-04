from fastapi import FastAPI, HTTPException
from handlers import register_device_token, delete_notification_token, get_participants, upload_profile_picture, confirm_profile_picture, delete_profile_picture

app = FastAPI()

@app.get("/health")
async def health_check():
    return {"status": "healthy"}

@app.get("/ready")
async def readiness_check():
    return {"status": "ready"}

app.include_router(register_device_token)
app.include_router(delete_notification_token)
app.include_router(get_participants)
app.include_router(upload_profile_picture)
app.include_router(confirm_profile_picture)
app.include_router(delete_profile_picture)

if __name__ == "__main__":
    import uvicorn
    uvicorn.run(app, host="0.0.0.0", port=8080)