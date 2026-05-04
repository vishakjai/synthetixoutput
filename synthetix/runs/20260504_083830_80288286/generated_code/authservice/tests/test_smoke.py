import requests

def test_health_check():
    response = requests.get("http://localhost:8080/health")
    assert response.status_code == 200
    assert response.json() == {"status": "healthy"}

def test_ready_check():
    response = requests.get("http://localhost:8080/ready")
    assert response.status_code == 200
    assert response.json() == {"status": "ready"}