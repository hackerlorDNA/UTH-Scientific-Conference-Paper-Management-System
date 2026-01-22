from fastapi import FastAPI
from pydantic import BaseModel
from typing import Optional

app = FastAPI(title="UTH-ConfMS AI Service")

class TextRequest(BaseModel):
    text: str

class SummaryResponse(BaseModel):
    summary: str
    key_points: list[str]

@app.get("/")
def read_root():
    return {"status": "AI Service is running"}

@app.post("/api/ai/summarize", response_model=SummaryResponse)
def summarize_text(request: TextRequest):
    # TODO: Integrate with Spacy or Ollama here
    # For now, return a dummy summary to prove connectivity
    summary = f"Processed summary for text length {len(request.text)}."
    return {
        "summary": summary,
        "key_points": ["Point 1", "Point 2"]
    }

@app.post("/api/ai/spellcheck")
def spell_check(request: TextRequest):
     # TODO: Integrate with Spacy
    return {"corrected_text": request.text, "corrections": []}
