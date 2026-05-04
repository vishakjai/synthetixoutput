from fastapi.testclient import TestClient
from main import app

client = TestClient(app)

def test_health_check():
    response = client.get("/health")
    assert response.status_code == 200
    assert response.json() == {"status": "healthy"}

def test_readiness_check():
    response = client.get("/ready")
    assert response.status_code == 200
    assert response.json() == {"status": "ready"}

def test_register_device_token():
    response = client.post("/api/notification/register", json={"username": "testuser", "email": "test@example.com", "password": "password", "recaptcha_token": "token"})
    assert response.status_code == 200
    assert response.json()["username"] == "testuser"

def test_delete_notification_token():
    response = client.delete("/api/notification/token123")
    assert response.status_code == 200
    assert response.json()["status"] == "deleted"