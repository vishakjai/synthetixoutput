from fastapi import APIRouter, HTTPException
from service import ChatService

router = APIRouter()
chat_service = ChatService()

@router.get("/api/chat/{chatId}/messages")
async def get_chat_messages(chatId: str):
    messages = chat_service.get_messages(chatId)
    if messages is None:
        raise HTTPException(status_code=404, detail="Chat not found")
    return {"status": "success", "messages": messages}

@router.get("/api/chat/{chatId}")
async def get_chat(chatId: str):
    chat = chat_service.get_chat(chatId)
    if chat is None:
        raise HTTPException(status_code=404, detail="Chat not found")
    return chat

@router.get("/api/chat")
async def list_chats():
    chats = chat_service.list_chats()
    return chats

@router.post("/api/chat")
async def create_chat(payload: dict):
    chat = chat_service.create_chat(payload)
    return chat

@router.post("/api/chat/{chatId}/add")
async def add_participant(chatId: str):
    success = chat_service.add_participant(chatId)
    if not success:
        raise HTTPException(status_code=404, detail="Chat not found")
    return {"status": "success"}

@router.delete("/api/chat/{chatId}/leave")
async def leave_chat(chatId: str):
    success = chat_service.leave_chat(chatId)
    if not success:
        raise HTTPException(status_code=404, detail="Chat not found")
    return {"status": "success"}
