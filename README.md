# 📷 Projeto de Processamento de Imagens em C#

Este projeto tem como objetivo demonstrar a aplicação de diversas técnicas de **processamento digital de imagens**, implementadas **sem o uso de bibliotecas externas**, utilizando apenas **C# puro**.  

As funcionalidades vão desde operações aritméticas simples até filtros espaciais e operações morfológicas, utilizando conceitos fundamentais de manipulação de pixels e convolução.

---

## 🛠️ Tecnologias Utilizadas

- **Linguagem:** C#
- **Framework:** .NET (Console ou Windows Forms)
- **Bibliotecas:** Nenhuma biblioteca externa de processamento de imagem foi utilizada (ex: OpenCV, AForge, EmguCV)

---

## 📁 Funcionalidades Implementadas

### 🔹 Parte Inicial

- Leitura de imagens nos formatos **BMP**, **JPG** e **PNG**, com armazenamento dos pixels em matrizes.
- Exibição da imagem resultante na interface.
- Salvamento da imagem final em arquivo.

---

### 🔸 Operações Aritméticas

- ✅ **Soma entre duas imagens**
- ✅ **Soma de constante (aumento de brilho)**  
  → Tratamento de **overflow**
- ✅ **Subtração entre duas imagens**
- ✅ **Subtração de constante (redução de brilho)**  
  → Tratamento de **underflow**
- ✅ **Multiplicação por constante (ajuste de contraste)**  
  → Tratamento de **overflow** e **underflow**
- ✅ **Divisão por constante (ajuste de contraste)**  
  → Tratamento de **overflow** e **underflow**

---

### 🔸 Conversões e Espelhamentos

- ✅ Conversão de imagem RGB para **escala de cinza**
- ✅ Espelhamento **horizontal** (esquerda ↔ direita)
- ✅ Espelhamento **vertical** (cima ↕ baixo)

---

### 🔸 Operações de Diferença e Combinação

- ✅ **Diferença** entre duas imagens
- ✅ **Combinação linear (blending)** entre imagens
- ✅ **Média** de duas imagens (combinação linear com pesos iguais)

---

### 🔸 Operações Lógicas (Imagens Binárias)

- ✅ **AND**
- ✅ **OR**
- ✅ **NOT**
- ✅ **XOR**

> ⚠️ Aplicáveis somente a imagens binárias.

---

### 🔸 Operações de Realce

- ✅ **Equalização de histograma**
- ✅ **Limiarização de imagens** (thresholding)

---

## 🧩 Filtragem no Domínio Espacial com Convolução

### 📉 Filtros Passa-Baixa

- ✅ **Filtro MAX**
- ✅ **Filtro MIN**
- ✅ **Filtro MÉDIA (MEAN)**
- ✅ **Filtro de mediana** (remoção de ruído sal e pimenta)
- ✅ **Filtro de ordem**
- ✅ **Filtro de suavização conservativa**
- ✅ **Filtro gaussiano**

---

### 📈 Filtros Passa-Alta (Detecção de Bordas)

#### Primeira Ordem:
- ✅ Filtro **Prewitt**
- ✅ Filtro **Sobel**

#### Segunda Ordem:
- ✅ Filtro **Laplaciano**

---

## ⚙️ Operações Morfológicas (Imagens Binárias)

- ✅ **Dilatação**
- ✅ **Erosão**
- ✅ **Abertura**
- ✅ **Fechamento**
- ✅ **Contorno**

---

## 📌 Observações

- Todas as implementações foram desenvolvidas do zero, sem bibliotecas auxiliares.
- O projeto é ideal para fins didáticos, aprendizado e análise de algoritmos clássicos de processamento de imagens.
- As imagens utilizadas como entrada devem ser compatíveis com os formatos suportados (BMP, JPG, PNG).

---

## 👨‍💻 Autor

Projeto desenvolvido por Felipe Meneguzzi como parte de estudos em Processamento Digital de Imagens e desenvolvimento com C#.

