class ChatService:
    def get_messages(self, chat_id):
        # Simulate fetching messages from a database
        return ["Hello", "World"] if chat_id == "1" else None

    def get_chat(self, chat_id):
        # Simulate fetching a chat
        return {"id": chat_id, "created_at": "2023-10-01", "updated_at": "2023-10-02"} if chat_id == "1" else None

    def list_chats(self, page, page_size):
        # Simulate listing chats
        return ([{"id": "1", "created_at": "2023-10-01", "updated_at": "2023-10-02"}], 1)

    def create_chat(self, payload):
        # Simulate chat creation
        return "2", "2023-10-03"

    def add_participant(self, chat_id):
        # Simulate adding a participant
        return chat_id == "1"

    def leave_chat(self, chat_id):
        # Simulate leaving a chat
        return chat_id == "1"