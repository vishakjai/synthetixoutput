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
async def list_chats(page: int = 1, page_size: int = 10):
    items, total = chat_service.list_chats(page, page_size)
    return {"items": items, "total": total, "page": page, "page_size": page_size}

@router.post("/api/chat")
async def create_chat(payload: dict):
    chat_id, created_at = chat_service.create_chat(payload)
    return {"id": chat_id, "created_at": created_at}

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