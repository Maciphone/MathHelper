import React, { useEffect, useState } from "react";
import Groq from "groq-sdk";

export default function AITextExercise() {
  const apiKey = import.meta.env.VITE_GROQ_API_KEY;
  const groq = new Groq({ apiKey });

  const [responseText, setResponseText] = useState("");

  useEffect(() => {
    async function main() {
      const chatCompletion = await groq.chat.completions.create({
        messages: [
          {
            role: "system",
            content: '["role"]: third grade teacher for mathematics',
          },
          {
            role: "user",
            content:
              "készíts játékos  szöveges matematika feladatot , aminek a megoldáshához az alábbi függvény:\n20 + X = 45; X=?\n felhasználása szúkséges" +
              "csak a feladat leírását add vissza válaszban",
          },
        ],
        model: "llama-3.1-70b-versatile",
        temperature: 1,
        max_tokens: 1024,
        top_p: 1,
        stream: true,
        stop: null,
      });

      let fullResponse = "";
      for await (const chunk of chatCompletion) {
        const content = chunk.choices[0]?.delta?.content || "";
        fullResponse += content;
        setResponseText(fullResponse);
      }
    }

    main();
  }, []);

  return (
    <div>
      <h1>AI Generált Matematika Feladat:</h1>
      <p>{responseText}</p>
    </div>
  );
}
