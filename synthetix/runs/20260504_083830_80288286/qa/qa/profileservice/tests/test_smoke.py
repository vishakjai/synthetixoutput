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

def test_execute_profile_action():
    response = client.post("/profile/execute")
    assert response.status_code == 200
    assert response.json() == {"message": "Profile action executed successfully"}
