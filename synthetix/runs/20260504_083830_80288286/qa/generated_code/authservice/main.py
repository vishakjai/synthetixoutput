from fastapi import FastAPI
from handlers import router
import os

app = FastAPI()

app.include_router(router)

@app.get("/health")
def health_check():
    return {"status": "healthy"}

@app.get("/ready")
def ready_check():
    return {"status": "ready"}

if __name__ == "__main__":
    port = int(os.getenv("PORT", 8080))
    import uvicorn
    uvicorn.run(app, host="0.0.0.0", port=port)