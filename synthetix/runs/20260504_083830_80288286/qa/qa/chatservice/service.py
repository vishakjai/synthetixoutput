class ChatService:
    def get_messages(self, chat_id):
        # Simulate fetching messages from a database
        return ["Hello", "World"] if chat_id == "1" else None

    def get_chat(self, chat_id):
        # Simulate fetching a chat from a database
        if chat_id == "1":
            return {"id": "1", "created_at": "2023-01-01T00:00:00Z", "updated_at": "2023-01-02T00:00:00Z"}
        return None

    def list_chats(self):
        # Simulate listing chats
        return {"items": [{"id": "1", "created_at": "2023-01-01T00:00:00Z", "updated_at": "2023-01-02T00:00:00Z"}], "total": 1, "page": 1, "page_size": 10}

    def create_chat(self, payload):
        # Simulate chat creation
        return {"id": "2", "created_at": "2023-01-03T00:00:00Z"}

    def add_participant(self, chat_id):
        # Simulate adding a participant
        return chat_id == "1"

    def leave_chat(self, chat_id):
        # Simulate leaving a chat
        return chat_id == "1"
