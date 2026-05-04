from fastapi.testclient import TestClient
from src.main import app

def test_health_check():
    client = TestClient(app)
    response = client.get("/health")
    assert response.status_code == 200
    assert response.json() == {"status": "healthy"}

def test_readiness_check():
    client = TestClient(app)
    response = client.get("/ready")
    assert response.status_code == 200
    assert response.json() == {"status": "ready"}

def test_execute_profile_action():
    client = TestClient(app)
    response = client.post("/profile/execute")
    assert response.status_code == 200
    assert response.json() == {"message": "Profile action executed"}
